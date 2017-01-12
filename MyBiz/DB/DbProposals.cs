using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MyBiz.DB
{
    public class DbProposals
    {
        private static SqlConnection Connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            SqlConnection con = new SqlConnection(constr);

            return con;
        }
        public static DataTable GetProposals()
        {            
            string query = "SELECT ID, ProposalName, DateSaved FROM Proposals ORDER BY DateSaved desc";
            
            SqlCommand cmd = new SqlCommand(query, Connection());
            Connection().Open();

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            Connection().Close();
            da.Dispose();

            return dt;
        }
    }
}