using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using SaludArTE.Entities;

namespace SaludArTE.Data.Mappers
{
    public class LogEntryMap : EntityTypeConfiguration<LogEntry>
    {
        public LogEntryMap()
        {
            this.HasKey(l => l.Id);
            this.Property(l => l.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
