using MyBiz.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyBiz
{
    public partial class Home : System.Web.UI.Page
    {
        public static DbUser AppUser = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                Response.Redirect("Login.aspx?ReturnUrl=Home.aspx");
                return;
            }
            var id = (FormsIdentity)HttpContext.Current.User.Identity;
            var username = id.Ticket.Name;
            AppUser = DbUser.Load(username);
            if (AppUser == null)
            {
                Response.Redirect("Login.aspx?ReturnUrl=Home.aspx");
                return;
            }
        }

        #region Logout
        [WebMethod]
        public static string Logout()
        {
            FormsAuthentication.SignOut();
            return "Login.aspx";
        }
        #endregion

        #region LoadHomeProposals
        [WebMethod]
        public static string LoadHomeProposals()
        {
            var dt = DbProposals.LoadTop3();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(dt);
        } 
        #endregion
    }
}