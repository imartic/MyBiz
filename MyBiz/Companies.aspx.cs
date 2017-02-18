using MyBiz.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyBiz
{
    public partial class Companies : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                Response.Redirect("Login.aspx?ReturnUrl=Companies.aspx");
                return;
            }
            var id = (FormsIdentity)HttpContext.Current.User.Identity;
            var username = id.Ticket.Name;
            Home.AppUser = DbUser.Load(username);
            if (Home.AppUser == null)
            {
                Response.Redirect("Login.aspx?ReturnUrl=Companies.aspx");
                return;
            }
        }

        #region LoadCompanies
        [WebMethod]
        public static string LoadCompanies()
        {
            var dt = DbCompany.LoadAll(Home.AppUser.ID);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(dt);
        }
        #endregion

        #region LoadCompany
        [WebMethod]
        public static string LoadCompany(int coId)
        {
            var co = DbCompany.Load(coId);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(co);
        }
        #endregion

        //#region DeleteProposal
        //[WebMethod]
        //public static string DeleteProposal(Int32 id)
        //{

        //}
        //#endregion
    }
}