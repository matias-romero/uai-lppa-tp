using System;

namespace SaludArTE.Entities
{
    /// <summary>
    /// Modela un registro de la bitácora
    /// </summary>
    public class LogEntry
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Username { get; set; }
        public string Description { get; set; }
    }
}
