using SaludArTE.BLL;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SaludArTE.Properties;

namespace SaludArTE.AdminPages
{
    public partial class BackupRestore : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var backupList = BackupManager.GetAvailableBackups();
                this.dtGridView.DataSource = backupList;
                this.dtGridView.DataBind();

                this.btnBackup.Text = Resources.BackupRestore_CreateBackup;

                if (Global.ModoMantenimiento)
                {
                    this.lblCorruptedDatabase.Visible = true;
                    var affectedEntities = (IEnumerable<string>)HttpContext.Current.Application["EstadoBaseDatos"];
                    foreach (var affectedEntity in affectedEntities)
                    {
                        var newListItem = new TagBuilder("li");
                        newListItem.SetInnerText(affectedEntity);
                        this.lblAffectedEntities.InnerHtml += newListItem.ToString();
                    }
                }
            }
        }

        protected void dtGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            var gridView = ((System.Web.UI.WebControls.GridView) sender);
            var backupPath = gridView.SelectedRow.Cells[1].Text;
            var restored = BackupManager.Restore(backupPath);
            if (restored)
            {
                //Cierro la sesión del usuario borrando el ticket de autenticación
                FormsAuthentication.SignOut();
                Response.Redirect(FormsAuthentication.LoginUrl);
            }
            this.lblAffectedEntities.InnerText = "Falló al restaurar el backup";
        }

        protected void btnBackup_OnClick(object sender, EventArgs e)
        {
            var backupName = (string)this.Request.Form["txtBackupName"];
            var result = BackupManager.Backup(backupName);
            this.ReloadCurrentPage();
        }

        private void ReloadCurrentPage()
        {
            Response.Redirect(Request.RawUrl);
        }
    }
}