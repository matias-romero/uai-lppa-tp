using System;
using System.Collections.Generic;

namespace SaludArTE.Models
{
    /// <summary>
    /// Modela un usuario de la aplicación
    /// </summary>
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public IList<string> Roles { get; set; }
        public string GivenName { get; set; }
    }
}