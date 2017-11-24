using SaludArTE.BLL;
using SaludArTE.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SaludArTE.Controllers
{
    public class AppointmentsController : ApiController
    {
        private readonly Appointments _appointmentsBll;

        public AppointmentsController(Appointments appointmentsBll)
        {
            _appointmentsBll = appointmentsBll;
        }

        // GET api/<controller>
        public IEnumerable<Appointment> Get()
        {
            return _appointmentsBll.GetAll();
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}