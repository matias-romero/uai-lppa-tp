using System;

namespace SaludArTE
{
    public partial class Common : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Global.ModoMantenimiento)
            {
                 if(!this.Page.AppRelativeVirtualPath.EndsWith("BackupRestore.aspx") && !this.Page.AppRelativeVirtualPath.EndsWith("Login.aspx"))
                    Response.Redirect("~/MaintenanceMode.aspx");
            }
                
        }
    }
}