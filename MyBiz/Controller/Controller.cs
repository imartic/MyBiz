using MyBiz.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace MyBiz.Controller
{
    public class Controller
    {
        [WebMethod]
        public static string loadProposals()
        {
            //Populating a DataTable from database.
            DataTable dt = DbProposals.GetProposals();

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            return serializer.Serialize(rows);


            //Building an HTML string.
            /* StringBuilder html = new StringBuilder();

             //Table start.
             //html.Append("<table border = '1'>");

             //Building the Header row.
             html.Append("<tr>");
             foreach (DataColumn column in dt.Columns)
             {
                 html.Append("<th>");
                 html.Append(column.ColumnName);
                 html.Append("</th>");
             }
             html.Append("</tr>");

             //Building the Data rows.
             foreach (DataRow row in dt.Rows)
             {
                 html.Append("<tr>");
                 foreach (DataColumn column in dt.Columns)
                 {
                     html.Append("<td>");
                     html.Append(row[column.ColumnName]);
                     html.Append("</td>");
                 }
                 html.Append("</tr>");
             }

             //Table end.
             html.Append("</table>");

             return html.ToString();*/
            //Append the HTML string to Placeholder.
            //PlaceHolder1.Controls.Add(new Literal { Text = html.ToString() });
        }
    }
}