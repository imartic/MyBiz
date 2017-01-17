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
        static SqlConnection con = Connection.getConnection();

        private static SqlCommand InitSqlCommand(String strProcedureName)
        {
            SqlCommand cmd = new SqlCommand(strProcedureName, con);
            cmd.CommandType = CommandType.StoredProcedure;

            return cmd;
        }

        private static void Execute(SqlCommand cmd, bool bCloseConn = true)
        {
            if (con != null && con.State == ConnectionState.Closed)
                con.Open();
            cmd.ExecuteNonQuery();
            if (bCloseConn) con.Close();
        }


        public static DataTable GetProposals()
        {            
            string query = "SELECT ID, ProposalName, DateSaved FROM Proposals ORDER BY DateSaved DESC, ID DESC";
            
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            con.Close();
            da.Dispose();

            return dt;
        }

        public static DataTable GetTop3Proposals()
        {
            string query = "SELECT TOP 3 ID, ProposalName, DateSaved FROM Proposals ORDER BY DateSaved DESC, ID DESC";

            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            con.Close();
            da.Dispose();

            return dt;
        }

        public static int SaveProposal(int proposalID, int userID, string proposalName, DateTime dateSaved, 
            /*int companyID,*/ string companyName)
        {
            try
            {
                SqlCommand cmd = InitSqlCommand("SaveProposal");

                if (proposalID > 0) cmd.Parameters.Add("@proposalID", SqlDbType.Int).Value = proposalID;
                else cmd.Parameters.Add("@proposalID", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.Parameters.Add("@userID", SqlDbType.Int).Value = userID;
                cmd.Parameters.Add("@proposalName", SqlDbType.NVarChar).Value = proposalName;
                cmd.Parameters.Add("@dateSaved", SqlDbType.DateTime).Value = dateSaved;
                //cmd.Parameters.Add("@companyID", SqlDbType.Int).Value = companyID;
                //cmd.Parameters.Add("@companyName", SqlDbType.NVarChar).Value = companyName;

                Execute(cmd);
                int id = Convert.ToInt32(cmd.Parameters["@proposalID"].Value);
                return id;
            }
            catch (Exception m)
            {
                throw m;
            }
        }
    }
}