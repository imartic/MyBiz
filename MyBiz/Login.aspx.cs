using MyBiz.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyBiz
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region LoginUser
        [WebMethod]
        public static string LoginUser(string username, string password, bool rememberMe)
        {
            if (IsAuthenticated(username, password))
            {
                //login ok, uzmi korisnika i spremi u session
                var user = DbUser.Load(username);
                if (user != null)
                {
                    var timeout = DateTime.Now.AddMinutes(20);
                    if (rememberMe)
                    {
                        timeout = DateTime.Now.AddYears(1);
                    }
                    FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, username, DateTime.Now, timeout, rememberMe, user.Permission.ToString());
                    string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                    HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

                    if (rememberMe) authCookie.Expires = authTicket.Expiration;
                    HttpContext.Current.Response.Cookies.Add(authCookie);

                    var url = "Home.aspx";
                    if (HttpContext.Current.Request.QueryString["ReturnUrl"] != null)
                    {
                        url = HttpContext.Current.Request.QueryString["ReturnUrl"];
                    }
                    return url;
                }
            }
            return "error";
        }
        #endregion

        #region IsAuthenticated
        private static bool IsAuthenticated(string username, string password)
        {
            var user = DbUser.Load(username);
            if ((user != null) && (user.Password.Length > 0))
            {
                return String.Compare(password, user.Password, false) == 0;
            }
            return false;
        }
        #endregion
    }
}