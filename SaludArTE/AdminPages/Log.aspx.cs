using System;
using System.IO;
using System.Net;
using System.Linq;

namespace SaludArTE.AdminPages
{
    public partial class Log : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            var host = string.Join("/", System.Web.HttpContext.Current.Request.Url.ToString().Split('/').Take(3));
            var url = host + Page.ResolveUrl("~/api/Logs");
            HttpWebRequest request = HttpWebRequest.CreateHttp(url);
            request.Accept ="application/xml";
            
            var response = request.GetResponse();

            var outputStream = File.OpenWrite(Server.MapPath(Page.ResolveUrl("~/App_Data/Export.xml")));
            response.GetResponseStream().CopyTo(outputStream);
            outputStream.Flush();
            outputStream.Close();
            //File.WriteAllBytes(Page.ResolveUrl("~/App_Data/Export.xml"), response.GetResponseStream().To);
            //Server.MapPath()
        }
    }
}