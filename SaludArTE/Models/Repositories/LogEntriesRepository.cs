using System;
using System.Collections.Generic;
using System.Linq;

namespace SaludArTE.Models.Repositories
{
    public interface ILogEntriesRepository
    {
        IEnumerable<LogEntry> GetAll();
        LogEntry Get(Guid id);
        void Update(LogEntry entry);
    }

    public class LogEntriesRepository : ILogEntriesRepository
    {
        private readonly List<LogEntry> _logEntries = new List<LogEntry>();

        public LogEntriesRepository()
        {
            _logEntries.Add(new LogEntry
            {
                Id = Guid.NewGuid(),
                Date = new DateTime(2017, 10, 18, 17, 52, 0),
                Username = "mromero",
                Description = "Intento de inicio de sesión fallido. Motivo: Contraseña Incorrecta"
            });
            _logEntries.Add(new LogEntry
            {
                Id = Guid.NewGuid(),
                Date = new DateTime(2017, 10, 17, 13, 00, 00),
                Username = "mromero",
                Description = "El usuario cerró la sesión"
            });
            _logEntries.Add(new LogEntry
            {
                Id = Guid.NewGuid(),
                Date = new DateTime(2017, 10, 17, 12, 30, 58),
                Username = "mromero",
                Description = "El usuario inició sesión correctamente"
            });
        }

        public IEnumerable<LogEntry> GetAll()
        {
            //Fake Sort
            return _logEntries.OrderByDescending(l => l.Date);
        }

        public LogEntry Get(Guid id)
        {
            var entry = _logEntries.Find(l => l.Id.Equals(id));
            return entry;
        }

        public void Update(LogEntry entry)
        {
            if (entry.Id.Equals(Guid.Empty))
            {
                entry.Id = Guid.NewGuid();
                _logEntries.Add(entry);
            }
            else //Ya existe, actualizo los datos nomás
            {
                var existingEntry = _logEntries.FirstOrDefault(l => l.Id.Equals(entry.Id));
                var currentIndex = _logEntries.IndexOf(existingEntry);
                _logEntries[currentIndex] = entry;
            }
        }
    }
}