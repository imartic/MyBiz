using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ABsistemLibrary.Data;

namespace MyBiz.Data
{
    [Serializable]
    public class DbItems : DbItem
    {
        public Int32 ID { get; set; }
        public Int32 ProposalID { get; set; }
        public string ItemNumber { get; set; }
        public string ItemText { get; set; }
        public string Unit { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? TotalPrice { get; set; }


    public DbItems()
			: base()		
		{
    }

    public DbItems(Int32 _ID, Int32 _ProposalID, string _ItemNumber, string _ItemText, string _Unit, decimal? _Quantity, decimal? _UnitPrice, decimal? _TotalPrice)
			: base()		
		{
        ID = _ID;
        ProposalID = _ProposalID;
        ItemNumber = _ItemNumber;
        ItemText = _ItemText;
        Unit = _Unit;
        Quantity = _Quantity;
        UnitPrice = _UnitPrice;
        TotalPrice = _TotalPrice;

    }

    public DbItems(byte[] byteArray, string[] keys = null)
			: base(byteArray, keys)
		{
    }

    private const string SQL = @"SELECT ID, ProposalID, ItemNumber, ItemText, Unit, Quantity, UnitPrice, TotalPrice FROM Items";
    public static DbCollection<DbItems> LoadAll(int proposalId, Dbase database = null)
    {
        var sql = SQL + @" WHERE ProposalID=@ProposalID ORDER BY ID";
        var di = new DbItem("ProposalID", proposalId);
        var result = DbaseTools.ExecuteQuery<DbItems>(database, sql, di);
        return result;
    }

    public static DbItems Load(string query, Dbase database = null)
    {
        //var sql = SQL + @" WHERE (Username=@Username) AND (Password=@Password) ORDER BY Name";
        //var di = new DbItem("Username", username, "Password", password);
        DbItems result = null;
        var dc = DbaseTools.ExecuteQuery<DbItems>(database/*, sql, di*/);
        if (dc.Count > 0) result = dc[0];
        return result;
    }

    public bool Delete(Int32 itemId, Dbase database = null)
    {
        var sql = @"DELETE FROM Items WHERE ID=@ID";
        var di = new DbItem("ID", itemId);
        return DbaseTools.ExecuteNonQuery(database, sql, di);
    }

    public bool Save(Dbase database = null)
    {
        var result = false;
        var sqlu = @"UPDATE Items SET ProposalID=@ProposalID, ItemNumber=@ItemNumber, ItemText=@ItemText, Unit=@Unit, Quantity=@Quantity, UnitPrice=@UnitPrice, TotalPrice=@TotalPrice WHERE ID=@ID";
        var sqli = @"INSERT INTO Items (ProposalID, ItemNumber, ItemText, Unit, Quantity, UnitPrice, TotalPrice) VALUES (@ProposalID, @ItemNumber, @ItemText, @Unit, @Quantity, @UnitPrice, @TotalPrice)";

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
            log.Error("DbItems, Save: error", ex);
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