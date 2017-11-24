using System;
using System.Collections.Generic;
using System.Linq;
using SaludArTE.Entities;

namespace SaludArTE.Data.Repositories
{
    public class AppointmentsRepository : IAppointmentsRepository
    {
        private IDbContext _context;

        public AppointmentsRepository(IDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Appointment> GetAll()
        {
            return _context.Appointments.ToArray();
        }

        public Appointment GetById(Guid id)
        {
            return _context.Appointments.Find(id);
        }

        public Appointment Create()
        {
            return _context.Appointments.Create();
        }

        public void Save(Appointment appointment)
        {
            if (appointment.Id.Equals(Guid.Empty))
                _context.Appointments.Add(appointment);
            else
                _context.Appointments.Attach(appointment);
        }
    }
}
