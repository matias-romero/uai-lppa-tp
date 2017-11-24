using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace SaludArTE.Models.Infrastructure
{
    /// <summary>
    /// Modela el usuario autenticado en la aplicación
    /// </summary>
    public class ApplicationPrincipal : GenericPrincipal
    {
        public ApplicationPrincipal(IIdentity identity, string[] roles) 
            : base(identity, roles)
        {
        }

        public Guid Sid
        {
            get { return Guid.Parse(this.FindFirst(c => c.Type == ClaimTypes.Sid).Value); }
        }

        public string GivenName
        {
            get { return this.FindFirst(c => c.Type == ClaimTypes.GivenName).Value; }
        }
    }
}