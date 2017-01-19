using ABsistemLibrary.Data;
using MyBiz.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace MyBiz
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo(Server.MapPath("~/Web.config")));

            var cs = System.Configuration.ConfigurationManager.AppSettings.Get("constr");
            DbaseTools.Start(1, "SQL", cs);
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                var username = authTicket.Name;
                var user = DbUser.Load(username);
                if ((user != null) && (user.Deleted == 0))
                {
                    var id = new FormsIdentity(authTicket);
                    var principal = new GenericPrincipal(id, null);
                    Context.User = principal;
                }
            }
        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {
            DbaseTools.Stop();
        }
    }
}