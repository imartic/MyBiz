using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyBiz
{
    public partial class NewEditProposal : System.Web.UI.Page
    {
        public class Proposal
        {
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
            Proposal obj = (new JavaScriptSerializer()).Deserialize<Proposal>(proposal);
            System.Diagnostics.Debug.WriteLine("company: " + obj.company);

            //todo: spremanje u bazu iz obj...
        }
    }
}