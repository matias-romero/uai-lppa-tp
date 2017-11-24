using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Web;
using System.IO;
using System.Text;
using System.Linq;
using Newtonsoft.Json;
using SaludArTE.Data.Repositories;

namespace SaludArTE.Models.Repositories
{
    public class AppointmentsRepository
    {
        public IEnumerable<Appointment> GetAll()
        {
            return this.LoadFromInternalFile()
                .Select(obj => MapFromJson((JObject)obj))
                //.Select(a => new { id = a.Id, title = a.Title, start = a.Start, end = a.End })
                .ToArray();
        }

        private JArray LoadFromInternalFile()
        {
            var filename = HttpContext.Current.Server.MapPath("~/App_Data/Appointments.json");
            return JArray.Parse(File.ReadAllText(filename, Encoding.UTF8));
        }

        private Appointment MapFromJson(JObject jsonAppointment)
        {
            return jsonAppointment == null ? null : JsonConvert.DeserializeObject<Appointment>(jsonAppointment.ToString());
        }
    }
}