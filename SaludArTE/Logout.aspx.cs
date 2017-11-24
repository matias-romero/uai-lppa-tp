using System;
using System.Web.Security;

namespace SaludArTE
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Request.HttpMethod == "POST")
                this.ExecuteLogout();

            Response.Redirect(FormsAuthentication.LoginUrl);
        }

        private void ExecuteLogout()
        {
            //Cierro la sesión del usuario borrando el ticket de autenticación
            FormsAuthentication.SignOut();
        }
    }
}