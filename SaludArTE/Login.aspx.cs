using System;
using System.Linq;
using System.Web.Security;
using SaludArTE.Models.Repositories;

namespace SaludArTE
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "Bienvenido";

            if (IsPostBack)
            {
                var rememberMeChecked = false;
                bool.TryParse(this.Request.Form["remember"], out rememberMeChecked);
                this.ExecuteLoginProcess(this.Request.Form["username"], this.Request.Form["password"], rememberMeChecked);
            }
        }

        private void ExecuteLoginProcess(string username, string password, bool rememberMe)
        {
            Global.ComprobarIntegridadBaseDeDatos();
            var usersRepository = this.RequestContainer().GetInstance<IUsersRepository>();
            var validatedUser = usersRepository.ValidateCredentials(username, password);
            if (validatedUser != null)
            {
                //Let us now set the authentication cookie so that we can use that later.
                FormsAuthentication.SetAuthCookie(validatedUser.Username, rememberMe);

                //Login successful lets put him to requested page
                var returnUrl = Request.QueryString["ReturnUrl"] as string;
                if (!string.IsNullOrWhiteSpace(returnUrl) && returnUrl.EndsWith(".aspx"))
                    Response.Redirect(returnUrl);
                else
                {
                    //no return URL specified so lets kick him to home page
                    if(validatedUser.Roles.Any(r => r.Equals("admin")))
                        Response.Redirect("~/AdminPages/Default.aspx");
                    if (validatedUser.Roles.Any(r => r.Equals("backend")))
                        Response.Redirect("~/BackendPages/Default.aspx");

                    Response.Redirect("~/Default.aspx");
                }
            }
            else
                this.validationSummary.Visible = true;
        }
    }
}