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

        [WebMethod]
        public static string SaveProposal(DbProposals proposal)
        {
            var database = DbaseTools.CreateDbase();
            var dbProposals = new DbProposals(proposal.ID, proposal.ProposalName, DateTime.Now, Home.AppUser.ID, 
                proposal.CompanyName, proposal.CompanyAddress, proposal.CompanyCity, proposal.CompanyPIN, proposal.CompanyPhone, proposal.CompanyFax,
                proposal.CompanyEmail, proposal.CompanyIBAN, proposal.ClientName, proposal.ClientAddress, proposal.ClientCity, proposal.ClientPhone,
                proposal.ClientEmail, proposal.ClientPIN);

            var result = dbProposals.Save(database);

            database.Close();
            database.Dispose();
            database = null;

            return result ? "OK" : "Greška pri spremanju u bazu podataka!";
        }

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