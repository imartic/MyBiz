using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ABsistemLibrary.Data;

namespace MyBiz.Data
{
    [Serializable]
    public class DbCompany : DbItem
    {
        public Int32 ID { get; set; }
        public Int32 UserID { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyCity { get; set; }
        public Int64? CompanyPIN { get; set; }
        public string CompanyPhone { get; set; }
        public string CompanyFax { get; set; }
        public string CompanyEmail { get; set; }
        public string CompanyIBAN { get; set; }


        public DbCompany()
            : base()
        {
        }

        public DbCompany(Int32 _ID, Int32 _UserID, string _CompanyName, string _CompanyAddress, string _CompanyCity, Int64? _CompanyPIN, string _CompanyPhone, string _CompanyFax, string _CompanyEmail, string _CompanyIBAN)
            : base()
        {
            ID = _ID;
            UserID = _UserID;
            CompanyName = _CompanyName;
            CompanyAddress = _CompanyAddress;
            CompanyCity = _CompanyCity;
            CompanyPIN = _CompanyPIN;
            CompanyPhone = _CompanyPhone;
            CompanyFax = _CompanyFax;
            CompanyEmail = _CompanyEmail;
            CompanyIBAN = _CompanyIBAN;

        }

        public DbCompany(byte[] byteArray, string[] keys = null)
            : base(byteArray, keys)
        {
        }

        private const string SQL = @"SELECT ID, UserID, CompanyName, CompanyAddress, CompanyCity, CompanyPIN, CompanyPhone, CompanyFax, CompanyEmail, CompanyIBAN FROM Company";
        public static DbCollection<DbCompany> LoadAll(int userId, Dbase database = null)
        {
            var sql = SQL + @" WHERE UserID=@UserID ORDER BY CompanyName";
            var di = new DbItem("UserID", userId);
            var result = DbaseTools.ExecuteQuery<DbCompany>(database, sql, di);
            return result;
        }

        public static DbCompany Load(int coId, Dbase database = null)
        {
            var sql = SQL + @" WHERE ID=@ID";
            var di = new DbItem("ID", coId);
            DbCompany result = null;
            var dc = DbaseTools.ExecuteQuery<DbCompany>(database, sql, di);
            if (dc.Count > 0) result = dc[0];
            return result;
        }

        public static bool Delete(Int32 id)
        {
            var sql = @"DELETE FROM Company WHERE ID=@ID";
            var di = new DbItem("ID", id);
            return DbaseTools.ExecuteNonQuery(null, sql, di);
        }

        public bool Save(Dbase database = null)
        {
            var result = false;
            var sqlu = @"UPDATE Company SET UserID=@UserID, CompanyName=@CompanyName, CompanyAddress=@CompanyAddress, CompanyCity=@CompanyCity, CompanyPIN=@CompanyPIN, CompanyPhone=@CompanyPhone, CompanyFax=@CompanyFax, CompanyEmail=@CompanyEmail, CompanyIBAN=@CompanyIBAN WHERE ID=@ID";
            var sqli = @"INSERT INTO Company (UserID, CompanyName, CompanyAddress, CompanyCity, CompanyPIN, CompanyPhone, CompanyFax, CompanyEmail, CompanyIBAN) VALUES (@UserID, @CompanyName, @CompanyAddress, @CompanyCity, @CompanyPIN, @CompanyPhone, @CompanyFax, @CompanyEmail, @CompanyIBAN)";

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
                log.Error("DbCompany, Save: error", ex);
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