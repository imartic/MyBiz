using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace MyBiz.Data
{
    class Connection
    {
        public static SqlConnection getConnection()
        {
            string constr = ConfigurationManager.AppSettings.Get("constr");
            SqlConnection con = new SqlConnection(constr);

            return con;
        }
    }
}
