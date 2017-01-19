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
        public DbUser AppUser = null;

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
            //Populating a DataTable from database.
            DataTable dt = DbProposals.GetTop3Proposals();

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            return serializer.Serialize(rows);
        } 
        #endregion
    }
}