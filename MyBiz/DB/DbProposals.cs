using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ABsistemLibrary.Data;

namespace MyBiz.Data
{
    [Serializable]
    public class DbProposals : DbItem
    {
        public Int32 ID { get; set; }
        public string ProposalName { get; set; }
        public DateTime DateSaved { get; set; }
        public Int32 UserID { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyCity { get; set; }
        public Int64? CompanyPIN { get; set; }
        public string CompanyPhone { get; set; }
        public string CompanyFax { get; set; }
        public string CompanyEmail { get; set; }
        public string CompanyIBAN { get; set; }
        public string ClientName { get; set; }
        public string ClientAddress { get; set; }
        public string ClientCity { get; set; }
        public string ClientPhone { get; set; }
        public string ClientEmail { get; set; }
        public Int64? ClientPIN { get; set; }


        public DbProposals()
                : base()
        {
        }

        public DbProposals(Int32 _ID, string _ProposalName, DateTime _DateSaved, Int32 _UserID, string _CompanyName, string _CompanyAddress, string _CompanyCity, Int64? _CompanyPIN, string _CompanyPhone, string _CompanyFax, string _CompanyEmail, string _CompanyIBAN, string _ClientName, string _ClientAddress, string _ClientCity, string _ClientPhone, string _ClientEmail, Int64? _ClientPIN)
                : base()
        {
            ID = _ID;
            ProposalName = _ProposalName;
            DateSaved = _DateSaved;
            UserID = _UserID;
            CompanyName = _CompanyName;
            CompanyAddress = _CompanyAddress;
            CompanyCity = _CompanyCity;
            CompanyPIN = _CompanyPIN;
            CompanyPhone = _CompanyPhone;
            CompanyFax = _CompanyFax;
            CompanyEmail = _CompanyEmail;
            CompanyIBAN = _CompanyIBAN;
            ClientName = _ClientName;
            ClientAddress = _ClientAddress;
            ClientCity = _ClientCity;
            ClientPhone = _ClientPhone;
            ClientEmail = _ClientEmail;
            ClientPIN = _ClientPIN;

        }

        public DbProposals(byte[] byteArray, string[] keys = null)
                : base(byteArray, keys)
        {
        }

        private const string SQL = @"SELECT ID, ProposalName, DateSaved, UserID, CompanyName, CompanyAddress, CompanyCity, CompanyPIN, CompanyPhone, CompanyFax, CompanyEmail, CompanyIBAN, ClientName, ClientAddress, ClientCity, ClientPhone, ClientEmail, ClientPIN FROM Proposals";
        public static DbCollection<DbProposals> LoadAll(int userId, Dbase database = null)
        {
            var sql = SQL + @" WHERE UserID=@UserID ORDER BY DateSaved DESC";
            var di = new DbItem("UserID", userId);
            var result = DbaseTools.ExecuteQuery<DbProposals>(database, sql, di);
            return result;
        }

        public static DbProposals Load(string query, Dbase database = null)
        {
            //var sql = SQL + @" WHERE (Username=@Username) AND (Password=@Password) ORDER BY Name";
            //var di = new DbItem("Username", username, "Password", password);
            DbProposals result = null;
            var dc = DbaseTools.ExecuteQuery<DbProposals>(database/*, sql, di*/);
            if (dc.Count > 0) result = dc[0];
            return result;
        }

        public static DbCollection<DbProposals> LoadTop3(int userId, Dbase database = null)
        {
            var sql = @"SELECT TOP 3 ID, ProposalName, DateSaved FROM Proposals WHERE UserID=@UserID ORDER BY DateSaved DESC";
            var di = new DbItem("UserID", userId);
            var result = DbaseTools.ExecuteQuery<DbProposals>(database, sql, di);
            return result;
        }

        public bool Delete()
        {
            var sql = @"DELETE FROM Proposals WHERE ID=@ID";
            return DbaseTools.ExecuteNonQuery(null, sql, this);
        }

        public bool Save(Dbase database = null)
        {
            var result = false;
            var sqlu = @"UPDATE Proposals SET ProposalName=@ProposalName, DateSaved=@DateSaved, CompanyName=@CompanyName, CompanyAddress=@CompanyAddress, CompanyCity=@CompanyCity, CompanyPIN=@CompanyPIN, CompanyPhone=@CompanyPhone, CompanyFax=@CompanyFax, CompanyEmail=@CompanyEmail, CompanyIBAN=@CompanyIBAN, ClientName=@ClientName, ClientAddress=@ClientAddress, ClientCity=@ClientCity, ClientPhone=@ClientPhone, ClientEmail=@ClientEmail, ClientPIN=@ClientPIN WHERE ID=@ID AND UserID=@UserID";
            var sqli = @"INSERT INTO Proposals (ProposalName, DateSaved, UserID, CompanyName, CompanyAddress, CompanyCity, CompanyPIN, CompanyPhone, CompanyFax, CompanyEmail, CompanyIBAN, ClientName, ClientAddress, ClientCity, ClientPhone, ClientEmail, ClientPIN) VALUES (@ProposalName, @DateSaved, @UserID, @CompanyName, @CompanyAddress, @CompanyCity, @CompanyPIN, @CompanyPhone, @CompanyFax, @CompanyEmail, @CompanyIBAN, @ClientName, @ClientAddress, @ClientCity, @ClientPhone, @ClientEmail, @ClientPIN)";

            var closeDb = (database == null);
            Db = DbaseTools.CreateDbase(database);
            try
            {
                if (!Db.Opened) Db.Open();
                result = DbaseTools.ExecuteNonQuery(Db, sqlu, this);
                if (!result)
                {
                    result = DbaseTools.ExecuteNonQuery(Db, sqli, this);
                    if (result)
                    {
                        SetValue("ID", Convert.ToInt32(Db.ExecuteScalar(@"SELECT @@IDENTITY")));
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("DbProposals, Save: error", ex);
            }
            if (closeDb)
            {
                Db.Close();
                Db.Dispose();
                Db = null;
            }
            return result;
        }
    }
}
