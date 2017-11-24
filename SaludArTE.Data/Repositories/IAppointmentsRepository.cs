using System;
using SaludArTE.Entities;
using System.Collections.Generic;

namespace SaludArTE.Data.Repositories
{
    public interface IAppointmentsRepository
    {
        IEnumerable<Appointment> GetAll();
        Appointment GetById(Guid id);
        Appointment Create();
        void Save(Appointment appointment);
    }
}