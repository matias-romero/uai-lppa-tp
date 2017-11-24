using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SaludArTE.Data.Repositories;

namespace SaludArTE.BLL
{
    public class LogEntries
    {
        private readonly ILogEntriesRepository _logEntriesRepository;

        public LogEntries(ILogEntriesRepository logEntriesRepository)
        {
            _logEntriesRepository = logEntriesRepository;
        }
    }
}