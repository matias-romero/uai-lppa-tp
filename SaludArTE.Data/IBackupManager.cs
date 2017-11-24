using System.Collections.Generic;

namespace SaludArTE.Data
{
    public interface IBackupManager
    {
        bool Backup(string name);
        IList<string> GetAvailableBackups();
        bool Restore(string name);
    }
}