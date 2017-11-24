using System;
using System.Collections.Generic;
using System.Linq;
using SaludArTE.Data.Repositories;

namespace SaludArTE.BLL
{
    public class Appointments
    {
        private readonly IAppointmentsRepository _appointmentsRepository;

        public Appointments(IAppointmentsRepository appointmentsRepository)
        {
            _appointmentsRepository = appointmentsRepository;
        }

        public IEnumerable<Models.Appointment> GetAll()
        {
            //Cargo de la base de datos y mapeo al modelo de vista
            return _appointmentsRepository.GetAll()
                .Select(a => new Models.Appointment
                {
                    Id = a.Id.ToString(),
                    Start = a.Start,
                    End = a.End,
                    Title = a.Title,
                    Url = a.Url
                });
        }

        public Models.Appointment CreateAppointment(DateTime dateTime, Guid patientId, Guid physicianId)
        {
            var vm = new Models.Appointment
            {
                Start = dateTime,
                End = dateTime.AddMinutes(10),
                Title = "Consulta con " + "Neurología",
                PatientId = patientId,
            };

            //TODO: Esta responsabilidad debería ser controller para hacer el mapeo entre VM -> DM
            var model = new Entities.Appointment
            {
                Start = vm.Start,
                End = vm.End,
                Title = vm.Title
            };
            _appointmentsRepository.Save(model);
            return vm;
        }
    }
}