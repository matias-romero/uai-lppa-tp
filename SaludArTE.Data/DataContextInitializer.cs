using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaludArTE.Entities;

namespace SaludArTE.Data
{
    public class DataContextInitializer : DropCreateDatabaseIfModelChanges<DbContext>
    {
        protected override void Seed(DbContext context)
        {
            var defaultAppointments = new List<Appointment>();

            defaultAppointments.Add(new Appointment() { Title = "Consulta Odontologia", Start = new DateTime(2017,11, 17, 10, 30, 00) });
            //defaultAppointments.Add(new Appointment() { Title = "Consulta Odontologia", Start = new DateTime(2017, 11, 17, 10, 30, 00) });
            //defaultAppointments.Add(new Appointment() { Title = "Consulta Odontologia", Start = new DateTime(2017, 11, 17, 10, 30, 00) });

            foreach (var std in defaultAppointments)
                context.Appointments.Add(std);

            base.Seed(context);
        }
    }
}
