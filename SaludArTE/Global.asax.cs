using System;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.Http;
using SaludArTE.Data.RedundancyCheck;
using SaludArTE.IoC;
using SaludArTE.Models.Infrastructure;
using SaludArTE.Models.Repositories;
using WebApi.StructureMap;

namespace SaludArTE
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.UseStructureMap(GlobalContainer.DefaultInstance.GetStructureMapContainer());
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            HttpContext.Current.Items["structuremap-container"] = GlobalContainer.DefaultInstance.GetNestedContainer();
        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            var containerPerRequest = (HttpContext.Current.Items["structuremap-container"] as IDisposable);
            if (containerPerRequest != null)
                containerPerRequest.Dispose();
        }

        protected void FormsAuthentication_OnAuthenticate(Object sender, FormsAuthenticationEventArgs e)
        {
            if (FormsAuthentication.CookiesSupported == true)
            {
                if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
                {
                    //let us take out the username now                
                    var encryptedTicketCookie = Request.Cookies[FormsAuthentication.FormsCookieName].Value;
                    var username = FormsAuthentication.Decrypt(encryptedTicketCookie).Name;

                    ////let us extract the roles from our own custom cookie
                    var usersRepository = CurrentContainer.GetInstance<IUsersRepository>();
                    var user = usersRepository.Get(username);

                    ////Let us set the Pricipal with our user specific details
                    var genericIdentity = new System.Security.Principal.GenericIdentity(user.Username, "Forms");
                    genericIdentity.AddClaim(new Claim(ClaimTypes.GivenName, user.GivenName));
                    genericIdentity.AddClaim(new Claim(ClaimTypes.Sid, user.Id.ToString()));
                    e.User = new ApplicationPrincipal(genericIdentity, user.Roles.ToArray());
                }
            }
        }

        public static bool ModoMantenimiento
        {
            get { return ((bool?) HttpContext.Current.Application["EstaEnMantenimiento"]).GetValueOrDefault(); }
            set { HttpContext.Current.Application["EstaEnMantenimiento"] = value; }
        }

        public static void ComprobarIntegridadBaseDeDatos()
        {
            //TODO: Esto es costoso así que sería mejor cachearlo e monitorearlo solo pasado cierto tiempo y no en cada login
            try
            {
                CurrentUnitOfWorkHelper.CheckDatabaseIntegrity();
                ModoMantenimiento = false;
            }
            catch (DatabaseCorruptedException ex)
            {
                ModoMantenimiento = true;
                HttpContext.Current.Application["EstadoBaseDatos"] = ex.AffectedEntities;
            }
            
        }

        public static IContainer CurrentContainer
        {
            get { return (IContainer)HttpContext.Current.Items["structuremap-container"]; }
        }

        public static Data.IUnitOfWorkHelper CurrentUnitOfWorkHelper
        {
            get{ return CurrentContainer.GetInstance<Data.IUnitOfWorkHelper>(); }
        }
    }
}