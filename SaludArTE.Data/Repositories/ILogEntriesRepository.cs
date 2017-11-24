using SaludArTE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaludArTE.Data.Repositories
{
    public interface ILogEntriesRepository
    {
        IEnumerable<LogEntry> GetAll();
    }
}
