using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ABsistemLibrary.Data;

namespace MyBiz.Data
{
    [Serializable]
    public class DbUser : DbItem
    {
        public Int32 ID { get; private set; }
        public string Role { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public string Email { get; private set; }
        public Int32 Permission { get; private set; }
        public Int32 Deleted { get; private set; }


        public enum EPermission
        {
            Normal = 0,
            Advanced = 1,
            Expert = 2
        }


        public DbUser()
            : base()
        {
        }

        public DbUser(Int32 _ID, string _Role, string _Username, string _Password, string _Email, Int32 _Permission, Int32 _Deleted)
            : base()
        {
            ID = _ID;
            Role = _Role;
            Username = _Username;
            Password = _Password;
            Email = _Email;
            Permission = _Permission;
            Deleted = _Deleted;

        }

        public DbUser(byte[] byteArray, string[] keys = null)
            : base(byteArray, keys)
        {
        }

        private const string SQL = @"SELECT ID, Role, Username, Password, Email, Permission, Deleted FROM [User]";
        public static DbCollection<DbUser> LoadAll(Dbase database = null)
        {
            var sql = SQL/* + @" ORDER BY Role"*/;
            var result = DbaseTools.ExecuteQuery<DbUser>(database, sql);
            return result;
        }

        public static DbUser Load(string username, Dbase database = null)
        {
            var sql = SQL + @" WHERE Username=@Username ORDER BY Role";
            var di = new DbItem("Username", username);
            DbUser result = null;
            var dc = DbaseTools.ExecuteQuery<DbUser>(database, sql, di);
            if (dc.Count > 0) result = dc[0];
            return result;
        }

        public bool Delete()
        {
            var sql = @"DELETE FROM User WHERE ID=@ID";
            return DbaseTools.ExecuteNonQuery(null, sql, this);
        }

        public bool Save(Dbase database = null)
        {
            var result = false;
            var sqlu = @"UPDATE [User] SET Role=@Role, Username=@Username, Password=@Password, Email=@Email, Permission=@Permission, Deleted=@Deleted WHERE ID=@ID";
            var sqli = @"INSERT INTO [User] (Role, Username, Password, Email, Permission, Deleted) VALUES (@Role, @Username, @Password, @Email, @Permission, @Deleted)";

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
                log.Error("DbUser, Save: error", ex);
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