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

namespace MyBiz
{
    public partial class NewEditProposal : System.Web.UI.Page
    {
        //public static int proposalID = 0;
        public static Proposal obj;
        public static Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

        public class Proposal
        {
            public int proposalID = 0;
            public string proposalName;
            public string company;
            public string companyAddress;
            public string companyPhone;
            public string companyPersonalNumber;
            public string client;
            public string clienAddress;
            public string clientPhone;
            public string clientPersonalNumber;
        }


        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static void SaveProposal(string proposal)
        {
            System.Diagnostics.Debug.WriteLine("data: " + proposal);
            obj = (new JavaScriptSerializer()).Deserialize<Proposal>(proposal);
            System.Diagnostics.Debug.WriteLine("proposal: " + obj.proposalName);

            //spremanje u bazu iz obj...
            obj.proposalID = DbProposals.SaveProposal(proposalID: obj.proposalID, userID: 1, proposalName: obj.proposalName, dateSaved: DateTime.Now.Date, companyName: obj.company);
        }

        [WebMethod]
        public static string[] ExportProposal()
        {
            string[] result = new string[2] { "", "" };

            if (obj == null || obj.proposalID == 0)
            {
                result[0] = "proposal not saved";
                return result;
            }

            if (xlApp == null)
            {
                result[0] = "excel not installed";
                return result;
            }

            var xlWorkBook = xlApp.Workbooks.Add();
            Excel.Range chartRange;

            var xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            xlWorkSheet.get_Range("b2", "e3").Merge(false);
            chartRange = xlWorkSheet.get_Range("b2", "e3");
            chartRange.FormulaR1C1 = obj.company + "\n" + obj.companyAddress + "\n" + obj.companyPhone;

            xlWorkBook.SaveAs("D:\\" + obj.proposalName + ".xls");

            xlWorkBook.Close(true);
            xlApp.Quit();

            if (File.Exists("D:\\" + obj.proposalName + ".xls"))
            {
                result[0] = "exported";
                result[1] = obj.proposalName.ToString() + ".xls";
            }

            Marshal.ReleaseComObject(xlWorkSheet);
            Marshal.ReleaseComObject(xlWorkBook);
            Marshal.ReleaseComObject(xlApp);

            return result;
        }
    }
}