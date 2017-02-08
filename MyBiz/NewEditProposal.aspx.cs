using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using MyBiz.Data;
using System.Web.Security;
using ABsistemLibrary.Data;

namespace MyBiz
{
    public partial class NewEditProposal : System.Web.UI.Page
    {
        //public static int proposalID = 0;
        //public static Proposal obj;
        public static Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

        public static object DbTicket { get; private set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                Response.Redirect("Login.aspx?ReturnUrl=NewEditProposals.aspx");
                return;
            }
            var id = (FormsIdentity)HttpContext.Current.User.Identity;
            var username = id.Ticket.Name;
            Home.AppUser = DbUser.Load(username);
            if (Home.AppUser == null)
            {
                Response.Redirect("Login.aspx?ReturnUrl=NewEditProposals.aspx");
                return;
            }
        }

        #region LoadProposal
        [WebMethod]
        public static string LoadProposal(Int32 id)
        {
            var proposal = DbProposals.Load(id);
            var json = new JavaScriptSerializer();
            return json.Serialize(proposal);
        }
        #endregion

        #region LoadProposalItems
        [WebMethod]
        public static string LoadProposalItems(Int32 id)
        {
            var items = DbItems.LoadAll(id);
            var json = new JavaScriptSerializer();
            return json.Serialize(items);
        }
        #endregion

        //#region DeleteItem
        //[WebMethod]
        //public static string DeleteItems(Int32[] items)
        //{
        //    var result = "";

        //    var db = DbaseTools.CreateDbase();
        //    try
        //    {
        //        db.Open();
        //        db.BeginTransaction();
        //        foreach (var id in items)
        //        {
        //            var dbItems = new DbItems();
        //            var res = dbItems.Delete(id, db);
        //            if (!res)
        //            {
        //                result = "Error on deleting item with ID = " + id + " from database!";
        //                db.Rollback();
        //                break;
        //            }

        //            result = "OK";
        //            db.Commit();
        //        }
        //    }
        //    catch
        //    {
        //        result = "Error on database!";
        //    }
        //    finally
        //    {
        //        db.Close();
        //        db.Dispose();
        //        db = null;
        //    }

        //    return result;
        //}
        //#endregion

        #region DeleteItem
        [WebMethod]
        public static bool DeleteItem(Int32 itemId)
        {
            var dbItems = new DbItems();
            var result = dbItems.Delete(itemId);
            return result;
        }
        #endregion

        #region SaveProposal
        [WebMethod]
        public static string SaveProposal(DbProposals proposal, DbItems[] items, int[] deletedItems)
        {
            var result = "";

            var dbProposals = new DbProposals(proposal.ID, proposal.ProposalName, DateTime.Now, Home.AppUser.ID,
                    proposal.CompanyName, proposal.CompanyAddress, proposal.CompanyCity, proposal.CompanyPIN, proposal.CompanyPhone, proposal.CompanyFax,
                    proposal.CompanyEmail, proposal.CompanyIBAN, proposal.ClientName, proposal.ClientAddress, proposal.ClientCity, proposal.ClientPhone,
                    proposal.ClientEmail, proposal.ClientPIN, proposal.ItemsTitle, proposal.Amount, proposal.Tax, proposal.Total, proposal.Note, proposal.Signature);

            var db = DbaseTools.CreateDbase();
            try
            {
                db.Open();
                db.BeginTransaction();

                var deleteResult = true;
                foreach(var id in deletedItems)
                {
                    deleteResult = DeleteItem(Convert.ToInt32(id));
                    if (!deleteResult)
                    {
                        db.Rollback();
                        result = "Error on deleting proposal item with ID = " + id + "!";
                        break;
                    }
                }

                var proposalId = dbProposals.Save(db);
                if (proposalId != -1)
                {
                    //prošao insert/update...
                    //update - postavi porposalId na id koji se prenosi iz proposal
                    if (proposalId == 0) proposalId = proposal.ID;

                    for (int i = 0; i < items.Length; i++)
                    {
                        var dbItems = new DbItems(items[i].ID, proposalId, items[i].ItemNumber, items[i].ItemText, items[i].Unit,
                            items[i].Quantity, items[i].UnitPrice, items[i].TotalPrice);

                        var res = dbItems.Save(db);

                        if (!res)
                        {
                            db.Rollback();
                            result = "Error on saving proposal items!";
                            break;
                        }
                    }

                    db.Commit();
                    result = "OK";
                }
                else
                {
                    db.Rollback();
                    result = "Error on saving proposal!";
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

            return result /*? "OK" : "Error saving proposal!"*/;
        } 
        #endregion

        //[WebMethod]
        //public static string[] ExportProposal()
        //{
        //string[] result = new string[2] { "", "" };

        //if (obj == null || obj.proposalID == 0)
        //{
        //    result[0] = "proposal not saved";
        //    return result;
        //}

        //if (xlApp == null)
        //{
        //    result[0] = "excel not installed";
        //    return result;
        //}

        //var xlWorkBook = xlApp.Workbooks.Add();
        //Excel.Range chartRange;

        //var xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
        //xlWorkSheet.get_Range("b2", "e3").Merge(false);
        //chartRange = xlWorkSheet.get_Range("b2", "e3");
        //chartRange.FormulaR1C1 = obj.company + "\n" + obj.companyAddress + "\n" + obj.companyPhone;

        //xlWorkBook.SaveAs("D:\\" + obj.proposalName + ".xls");

        //xlWorkBook.Close(true);
        //xlApp.Quit();

        //if (File.Exists("D:\\" + obj.proposalName + ".xls"))
        //{
        //    result[0] = "exported";
        //    result[1] = obj.proposalName.ToString() + ".xls";
        //}

        //Marshal.ReleaseComObject(xlWorkSheet);
        //Marshal.ReleaseComObject(xlWorkBook);
        //Marshal.ReleaseComObject(xlApp);

        //return result;
        //}
    }
}