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
        public string ItemsTitle { get; set; }


        public DbProposals()
                : base()
        {
        }

        public DbProposals(Int32 _ID, string _ProposalName, DateTime _DateSaved, Int32 _UserID, string _CompanyName, string _CompanyAddress, string _CompanyCity, Int64? _CompanyPIN, string _CompanyPhone, string _CompanyFax, string _CompanyEmail, string _CompanyIBAN, string _ClientName, string _ClientAddress, string _ClientCity, string _ClientPhone, string _ClientEmail, Int64? _ClientPIN, string _ItemsTitle)
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
            ItemsTitle = _ItemsTitle;

        }

        public DbProposals(byte[] byteArray, string[] keys = null)
                : base(byteArray, keys)
        {
        }

        private const string SQL = @"SELECT ID, ProposalName, DateSaved, UserID, CompanyName, CompanyAddress, CompanyCity, CompanyPIN, CompanyPhone, CompanyFax, CompanyEmail, CompanyIBAN, ClientName, ClientAddress, ClientCity, ClientPhone, ClientEmail, ClientPIN, ItemsTitle FROM Proposals";
        public static DbCollection<DbProposals> LoadAll(int userId, Dbase database = null)
        {
            var sql = SQL + @" WHERE UserID=@UserID ORDER BY DateSaved DESC";
            var di = new DbItem("UserID", userId);
            var result = DbaseTools.ExecuteQuery<DbProposals>(database, sql, di);
            return result;
        }

        public static DbProposals Load(Int32 id, Dbase database = null)
        {
            var sql = SQL + @" WHERE ID=@ID";
            var di = new DbItem("ID", id);
            DbProposals result = null;
            var dc = DbaseTools.ExecuteQuery<DbProposals>(database, sql, di);
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

        public bool Delete(int id, Dbase database = null)
        {
            var sql = @"DELETE FROM Proposals WHERE ID=@ID";
            var di = new DbItem("ID", id);
            return DbaseTools.ExecuteNonQuery(database, sql, di);
        }

        public int Save(Dbase database = null)
        {
            var result = false;
            var id = 0;
            var sqlu = @"UPDATE Proposals SET ProposalName=@ProposalName, DateSaved=@DateSaved, CompanyName=@CompanyName, CompanyAddress=@CompanyAddress, CompanyCity=@CompanyCity, CompanyPIN=@CompanyPIN, CompanyPhone=@CompanyPhone, CompanyFax=@CompanyFax, CompanyEmail=@CompanyEmail, CompanyIBAN=@CompanyIBAN, ClientName=@ClientName, ClientAddress=@ClientAddress, ClientCity=@ClientCity, ClientPhone=@ClientPhone, ClientEmail=@ClientEmail, ClientPIN=@ClientPIN, ItemsTitle=@ItemsTitle WHERE ID=@ID AND UserID=@UserID";
            var sqli = @"INSERT INTO Proposals (ProposalName, DateSaved, UserID, CompanyName, CompanyAddress, CompanyCity, CompanyPIN, CompanyPhone, CompanyFax, CompanyEmail, CompanyIBAN, ClientName, ClientAddress, ClientCity, ClientPhone, ClientEmail, ClientPIN, ItemsTitle) VALUES (@ProposalName, @DateSaved, @UserID, @CompanyName, @CompanyAddress, @CompanyCity, @CompanyPIN, @CompanyPhone, @CompanyFax, @CompanyEmail, @CompanyIBAN, @ClientName, @ClientAddress, @ClientCity, @ClientPhone, @ClientEmail, @ClientPIN, @ItemsTitle)";

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
                        id = Convert.ToInt32(Db.ExecuteScalar(@"SELECT @@IDENTITY"));
                        SetValue("ID", id);
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
            //return result;
            return (result == false) ? -1 : id;
        }
    }
}
