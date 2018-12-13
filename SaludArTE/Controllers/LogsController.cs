using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using SaludArTE.Models;
using SaludArTE.Models.Repositories;

namespace SaludArTE.Controllers
{
    [AllowAnonymous]
    public class LogsController : ApiController
    {
        private readonly ILogEntriesRepository _repository;

        public LogsController()
            :this(new LogEntriesRepository())
        {
        }

        public LogsController(ILogEntriesRepository repository)
        {
            _repository = repository;
        }

        // GET api/<controller>
        public IEnumerable<LogEntry> Get()
        {
            return _repository.GetAll().ToArray();
        }

        // GET api/<controller>/5
        public LogEntry Get(Guid id)
        {
            var entry = _repository.Get(id);
            return entry;
        }

        // POST api/<controller>
        public void Post(LogEntry value)
        {
            //TODO
        }

        // PUT api/<controller>/5
        public void Put(LogEntry value)
        {
            if (ModelState.IsValid)
            {
                value.Id = Guid.Empty; //Evito un posible OverPosting
                _repository.Update(value);
            }
        }

        // DELETE api/<controller>/5
        public void Delete(Guid id)
        {
            //TODO
        }
    }
}