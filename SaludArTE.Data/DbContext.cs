using System;
using SaludArTE.Data.Mappers;
using SaludArTE.Entities;
using System.Data.Entity;
using SaludArTE.Data.RedundancyCheck;
using SaludArTE.Entities.RedundancyCheck;

namespace SaludArTE.Data
{
    public interface IDbContext
    {
        IDbSet<User> Users { get; }
        IDbSet<Appointment> Appointments { get; }
        IDbSet<LogEntry> LogEntries { get; }
        IDbSet<VerticalCheckDigit> CheckDigits { get; }

        int SaveChanges();
    }

    public class DbContext : DbContextWithRedundancyChecking, IDbContext
    {
        static DbContext()
        {
            Database.SetInitializer(new DataContextInitializer());
        }

        public DbContext(IRedundancyCheckAnalyzer redundancyCheckAnalyzer)
            :base("ApplicationDbContext", redundancyCheckAnalyzer)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new AppointmentMap());
            modelBuilder.Configurations.Add(new LogEntryMap());
            modelBuilder.Configurations.Add(new VerticalCheckDigitMap());
        }

        public IDbSet<User> Users { get; set; }

        public IDbSet<Appointment> Appointments { get; set; }

        public IDbSet<LogEntry> LogEntries { get; set; }

        public IDbSet<VerticalCheckDigit> CheckDigits { get; set; }
    }
}
