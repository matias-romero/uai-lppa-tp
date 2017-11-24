using SaludArTE.Data;
using System.Collections.Generic;

namespace SaludArTE.BLL
{
    public static class BackupManager
    {
        public static bool Backup(string name)
        {
            var backupManager = GetBackupManager();
            return backupManager.Backup(name);
        }

        public static IList<string> GetAvailableBackups()
        {
            var backupManager = GetBackupManager();
            return backupManager.GetAvailableBackups();
        }

        public static bool Restore(string name)
        {
            var backupManager = GetBackupManager();
            return backupManager.Restore(name);
        }

        private static IBackupManager GetBackupManager()
        {
            return Global.CurrentUnitOfWorkHelper.BackupManager;
        }
    }
}