using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using SaludArTE.Data.RedundancyCheck;
using SaludArTE.Entities.RedundancyCheck;

namespace SaludArTE.Data
{
    public interface IUnitOfWorkHelper : IDisposable
    {
        IDbContext DbContext { get; }
        IBackupManager BackupManager { get; }
        void CheckDatabaseIntegrity();
    }

    /// <summary>
    /// Modela el patrón Unit Of Work para contener las transacciones a nivel de Db
    /// </summary>
    public class UnitOfWorkHelper : IUnitOfWorkHelper
    {
        private readonly Lazy<IDbContext> _dbContext;
        private readonly IRedundancyCheckAnalyzer _redundancyCheckAnalyzer = new RedundancyCheckAnalyzer();
        public UnitOfWorkHelper()
        {
            _dbContext = new Lazy<IDbContext>(() => new DbContext(_redundancyCheckAnalyzer));
        }

        public IDbContext DbContext
        {
            get { return _dbContext.Value; }
        }

        public IBackupManager BackupManager
        {
            get { return new SqlServerBackupManager(this.DbContext); }
        }


        public void CheckDatabaseIntegrity()
        {
            var corruptedEntityNames = new List<string>();
            var entityWithRedundancyCheckType = typeof(IEntityWithRedundancyCheck);
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(typeof(IDbContext)))
            {
                if (property.PropertyType.IsConstructedGenericType)
                {
                    var entityType = property.PropertyType.GetGenericArguments()[0];
                    var isEntityWithRedundancyCheck = entityWithRedundancyCheckType.IsAssignableFrom(entityType);
                    if (isEntityWithRedundancyCheck) //Debo validar los dígitos verificadores de dicha entidad
                    {
                        var entityDbSet = (IQueryable)property.GetValue(this.DbContext);
                        if(this.IsEntityCorrupted(entityDbSet, entityType))
                            corruptedEntityNames.Add(entityType.FullName);
                    }
                }
            }

            //Reviento si al menos existe una entidad corrupta
            if(corruptedEntityNames.Any())
                throw new DatabaseCorruptedException(corruptedEntityNames);
        }

        private bool IsEntityCorrupted(IQueryable entityDbSet, Type entityType)
        {
            var crcs = new List<byte[]>();
            foreach (IEntityWithRedundancyCheck entity in entityDbSet.AsNoTracking())
            {
                if (!_redundancyCheckAnalyzer.IsValid(entity)) //Encontre un registro corrupto
                    return true;

                crcs.Add(entity.CRC);
            }

            //Calculo el hash vertical con todos los CRC individuales y lo comparo con lo que tengo almacenado
            var verticalChecksum = _redundancyCheckAnalyzer.CalculateHashFromMultipleCRCs(crcs);
            var savedVerticalDigitCheck = this.DbContext.CheckDigits.Find(entityType.FullName);
            return !verticalChecksum.SequenceEqual(savedVerticalDigitCheck.Checksum);
        }

        #region IDisposable Support
        private bool disposedValue = false; // Para detectar llamadas redundantes

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (_dbContext.IsValueCreated)
                        ((IDisposable)_dbContext.Value).Dispose();
                    // TODO: elimine el estado administrado (objetos administrados).
                }

                // TODO: libere los recursos no administrados (objetos no administrados) y reemplace el siguiente finalizador.
                // TODO: configure los campos grandes en nulos.

                disposedValue = true;
            }
        }

        // TODO: reemplace un finalizador solo si el anterior Dispose(bool disposing) tiene código para liberar los recursos no administrados.
        // ~UnitOfWorkHelper() {
        //   // No cambie este código. Coloque el código de limpieza en el anterior Dispose(colocación de bool).
        //   Dispose(false);
        // }

        // Este código se agrega para implementar correctamente el patrón descartable.
        public void Dispose()
        {
            // No cambie este código. Coloque el código de limpieza en el anterior Dispose(colocación de bool).
            Dispose(true);
            // TODO: quite la marca de comentario de la siguiente línea si el finalizador se ha reemplazado antes.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
