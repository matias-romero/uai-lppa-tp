using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data;

namespace SaludArTE.Data
{
    public class SqlServerBackupManager : IBackupManager
    {
        private Database _currentDatabase;
        private SqlConnectionStringBuilder _cnnStringBuilder;
        private string _databaseName;
        private IDbConnection _masterDbConnection;

        public SqlServerBackupManager(IDbContext context)
        {
            var dbContext = (DbContext)context;
            _currentDatabase = dbContext.Database;
            _currentDatabase.Connection.Open();
            _databaseName = _currentDatabase.Connection.Database;
            _currentDatabase.Connection.Close();

            //Cambio a la master asociada
            _cnnStringBuilder = new SqlConnectionStringBuilder(_currentDatabase.Connection.ConnectionString);
            _cnnStringBuilder.AttachDBFilename = "";
            var masterCnnString = _cnnStringBuilder.ToString();
            _masterDbConnection = new System.Data.SqlClient.SqlConnection(masterCnnString);
        }

        public bool Backup(string name)
        {
            var sql = string.Format("BACKUP DATABASE [{0}] TO DISK = '{1}_{2:yyyyMMddhhmmss}.bak'", _databaseName, name, DateTime.Now);
            _currentDatabase.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, sql);
            return true;
        }

        public bool Restore(string name)
        {
            _masterDbConnection.Open();
            try
            {
                var sql = string.Format("ALTER DATABASE [{0}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE; RESTORE DATABASE [{0}] FROM DISK = '{1}' WITH RECOVERY", _databaseName, name);
                var command = _masterDbConnection.CreateCommand();
                command.CommandText = sql;
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if(_masterDbConnection.State != ConnectionState.Closed)
                    _masterDbConnection.Close();
            }

            return true;
        }

        public IList<string> GetAvailableBackups()
        {
            string sql;
            using (var stream = this.GetType().Assembly.GetManifestResourceStream(this.GetType(), "Scripts.ListAvailableBackups.sql"))
                sql = new System.IO.StreamReader(stream).ReadToEnd();

            var backupSetInfos = _currentDatabase.SqlQuery<BackupSetInfo>(sql).ToArray();
            return backupSetInfos.Select(b => b.physical_device_name).ToList();
        }

        private class BackupSetInfo
        {
            public string Server { get; set; }
            public string physical_device_name { get; set; }
        }

    }
}
