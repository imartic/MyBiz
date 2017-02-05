using ABsistemLibrary.Data;
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
    public partial class Proposals : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                Response.Redirect("Login.aspx?ReturnUrl=Proposals.aspx");
                return;
            }
            var id = (FormsIdentity)HttpContext.Current.User.Identity;
            var username = id.Ticket.Name;
            Home.AppUser = DbUser.Load(username);
            if (Home.AppUser == null)
            {
                Response.Redirect("Login.aspx?ReturnUrl=Proposals.aspx");
                return;
            }
        }

        #region LoadProposals
        [WebMethod]
        public static string LoadProposals()
        {
            var dt = DbProposals.LoadAll(Home.AppUser.ID);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(dt);
        } 
        #endregion

        #region DeleteProposal
        [WebMethod]
        public static string DeleteProposal(Int32 id)
        {
            var result = "";
            var dbProposal = new DbProposals();

            var db = DbaseTools.CreateDbase();
            try
            {
                db.Open();
                db.BeginTransaction();

                var delItems = true;
                var items = DbItems.LoadAll(id, db);
                if (items.Count > 0)
                {                    
                    var dbItems = new DbItems();
                    foreach (var i in items)
                    {
                        delItems = dbItems.Delete(i.ID, db);
                        if (!delItems)
                        {
                            db.Rollback();
                            result = "Error on deleting proposal item " + i.ID + "!";
                            break;
                        }
                    }
                }

                if (delItems)
                {
                    var delProposal = dbProposal.Delete(id, db);
                    if (!delProposal)
                    {
                        db.Rollback();
                        result = "Error on deleting proposal!";
                    }

                    db.Commit();
                    result = "OK";
                }                
            }
            catch
            {
                result = "Error on database!";
            }
            finally
            {
                db.Close();
                db.Dispose();
                db = null;
            }

            return result;
        }
        #endregion
    }
}