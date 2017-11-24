using System;
using System.Web;
using System.Web.UI;
using SaludArTE.Data;
using SaludArTE.IoC;
using SaludArTE.Models.Infrastructure;

namespace SaludArTE
{
    public static class PageExtensions
    {
        /// <summary>
        /// Devuelve el contenedor utilizado en este request para la inyección de dependencias
        /// </summary>
        /// <param name="thisPage"></param>
        /// <returns></returns>
        public static IContainer RequestContainer(this Page thisPage)
        {
            return Global.CurrentContainer;
        }

        /// <summary>
        /// Devuelve la unidad de trabajo correspondiente al request en curso
        /// </summary>
        /// <param name="thisPage"></param>
        /// <returns></returns>
        public static IUnitOfWorkHelper CurrentUnitOfWork(this Page thisPage)
        {
            return Global.CurrentUnitOfWorkHelper;
        }

        /// <summary>
        /// Devuelve el usuario logueado actualmente en el sistema
        /// </summary>
        /// <param name="thisPage"></param>
        /// <returns></returns>
        public static ApplicationPrincipal CurrentUser(this Page thisPage)
        {
            return thisPage.User as ApplicationPrincipal;
        }
    }
}