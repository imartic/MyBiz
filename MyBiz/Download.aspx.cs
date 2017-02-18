using ABsistemLibrary.Data;
using MyBiz.Data;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyBiz
{
    public partial class Download : System.Web.UI.Page
    {
        int proposalId = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            var id = Request.QueryString[0];
            System.Diagnostics.Debug.WriteLine("urlId = " + id);

            if (Int32.TryParse(id, out proposalId))
            {
                using (var database = DbaseTools.CreateDbase())
                {
                    var proposal = DbProposals.Load(proposalId);
                    //var items = DbItems.LoadAll(proposalId);

                    if (proposal != null)
                    {
                        if (database.Opened) database.Close();

                        CreateExcel(proposal);
                    }
                }
            }
        }

        private void CreateExcel(DbProposals proposal)
        {
            var fileName = proposal.ProposalName;

            using (ExcelPackage pck = new ExcelPackage())
            {
                //Create the worksheet
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add(fileName);


                //Company
                var aRow = 2;
                ws.Cells["A1"].Value = proposal.CompanyName;
                if (proposal.CompanyAddress != "" && proposal.CompanyAddress != null)
                {
                    ws.Cells["A" + aRow].Value = proposal.CompanyAddress;
                    aRow++;
                }
                if (proposal.CompanyCity != "" && proposal.CompanyCity != null)
                {
                    ws.Cells["A" + aRow].Value = proposal.CompanyCity;
                    aRow++;
                }
                if (proposal.CompanyPIN.ToString() != "" && proposal.CompanyPIN != null)
                {
                    ws.Cells["A" + aRow].Value = "OIB: " + proposal.CompanyPIN.ToString();
                }

                var dRow = 1;
                if (proposal.CompanyPhone != "" && proposal.CompanyPhone != null)
                {
                    ws.Cells["D" + dRow].Value = "TEL.: " + proposal.CompanyPhone;
                    dRow++;
                }
                if (proposal.CompanyFax != "" && proposal.CompanyFax != null)
                {
                    ws.Cells["D" + dRow].Value = "FAX: " + proposal.CompanyFax;
                    dRow++;
                }
                if (proposal.CompanyEmail != "" && proposal.CompanyEmail != null)
                {
                    ws.Cells["D" + dRow].Value = "E-MAIL: " + proposal.CompanyEmail;
                    dRow++;
                }
                if (proposal.CompanyIBAN != "" && proposal.CompanyIBAN != null)
                {
                    ws.Cells["D" + dRow].Value = "IBAN: " + proposal.CompanyIBAN;
                }


                for (int i = 1; i <= aRow; i++)
                {
                    ws.Cells["A" + i + ":B" + i].Merge = true;
                }

                for (int i = 1; i <= dRow; i++)
                {
                    ws.Cells["D" + i + ":F" + i].Merge = true;
                }

                ws.Cells["A" + ((aRow >= dRow) ? aRow : dRow) + ":F" + ((aRow >= dRow) ? aRow : dRow)].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                ws.Cells["D1:F" + dRow].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                ws.Cells["A1:D" + aRow].Style.Font.Bold = true;


                //Client
                var clBeginRow = (aRow >= dRow) ? aRow + 5 : dRow + 5;
                var clRow = clBeginRow + 1;
                ws.Cells["A" + (clBeginRow)].Value = proposal.ClientName;
                if (proposal.ClientAddress != "" && proposal.ClientAddress != null)
                {
                    ws.Cells["A" + clRow].Value = proposal.ClientAddress;
                    clRow++;
                }
                if (proposal.ClientCity != "" && proposal.ClientCity != null)
                {
                    ws.Cells["A" + clRow].Value = proposal.ClientCity;
                    clRow++;
                }
                if (proposal.ClientPhone != "" && proposal.ClientPhone != null)
                {
                    ws.Cells["A" + clRow].Value = "TEL.: " + proposal.ClientPhone;
                    clRow++;
                }
                if (proposal.ClientEmail != "" && proposal.ClientEmail != null)
                {
                    ws.Cells["A" + clRow].Value = "E-MAIL: " + proposal.ClientEmail;
                    clRow++;
                }
                if (proposal.ClientPIN.ToString() != "" && proposal.ClientPIN != null)
                {
                    ws.Cells["A" + clRow].Value = "OIB:" + proposal.ClientPIN.ToString();
                }


                for (int i = clBeginRow; i <= clRow; i++)
                {
                    ws.Cells["A" + i + ":F" + i].Merge = true;
                }

                ws.Cells["A" + clBeginRow + ":A" + clRow].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                //ws.Cells["E9:F9"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                //ws.Cells["F9:F14"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                //ws.Cells["E14:F14"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                //ws.Cells["E9:F14"].Style.Border.Left.Style = ExcelBorderStyle.Thin;


                //Items title
                var itRow = clRow + 4;
                ws.Cells["A" + itRow + ":F" + itRow].Merge = true;
                ws.Cells["A" + itRow].Value = proposal.ItemsTitle;
                ws.Cells["A" + itRow].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells["A" + itRow].Style.Font.UnderLine = true;

                ws.Cells["A" + (itRow + 3)].Value = "BR."; //todo: jezik proslijediti iz postavki...
                ws.Cells["B" + (itRow + 3)].Value = "NAZIV ARTIKLA/USLUGE";
                ws.Cells["C" + (itRow + 3)].Value = "J. MJ.";
                ws.Cells["D" + (itRow + 3)].Value = "KOL.";
                ws.Cells["E" + (itRow + 3)].Value = "CIJENA";
                ws.Cells["F" + (itRow + 3)].Value = "VRIJEDNOST";

                ws.Cells["A" + (itRow + 3) + ":F" + (itRow + 3)].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                //Items
                var items = DbItems.LoadAll(proposalId);
                var row = itRow + 4;
                if (items != null && items.Count > 0)
                {
                    for (int i = 0; i < items.Count; i++)
                    {
                        ws.Cells["A" + row].Value = items[i].ItemNumber;
                        ws.Cells["B" + row].Value = items[i].ItemText;
                        ws.Cells["C" + row].Value = items[i].Unit;
                        ws.Cells["D" + row].Value = items[i].Quantity;
                        ws.Cells["E" + row].Value = items[i].UnitPrice;
                        ws.Cells["F" + row].Value = items[i].TotalPrice;

                        ws.Row(row).Style.Fill.PatternType = ExcelFillStyle.Solid;
                        ws.Row(row).Style.Fill.BackgroundColor.SetColor((row % 2 == 0) ? Color.Transparent : Color.FromArgb(240, 240, 240));

                        if (i < (items.Count - 1)) row++;
                    }
                    ws.Cells["D" + (itRow + 4) + ":F" + row].Style.Numberformat.Format = "0.00";

                    ws.Cells["A" + row + ":F" + row].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;


                    //Total
                    ws.Cells["E" + (row + 2)].Value = "IZNOS:";
                    ws.Cells["F" + (row + 2)].Value = proposal.Amount + " kn"; //todo: valutu proslijediti iz postavki...
                    ws.Cells["F" + (row + 2)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                    ws.Cells["F" + (row + 2)].Style.Numberformat.Format = "0.00";
                    if (proposal.Tax > 0)
                    {
                        ws.Cells["E" + (row + 3)].Value = "PDV:";
                        ws.Cells["F" + (row + 3)].Value = proposal.Tax + " kn";
                        ws.Cells["F" + (row + 3)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        ws.Cells["F" + (row + 3)].Style.Numberformat.Format = "0.00";
                    }
                    else
                    {
                        ws.Cells["E" + (row + 3) + ":F" + (row + 3)].Merge = true;
                        ws.Cells["E" + (row + 3)].Value = "PDV nije obračunat";
                    }
                    ws.Cells["E" + (row + 4)].Value = "UKUPNO:";
                    ws.Cells["F" + (row + 4)].Value = proposal.Total + " kn";
                    ws.Cells["F" + (row + 4)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                    ws.Cells["F" + (row + 4)].Style.Numberformat.Format = "0.00";

                    ws.Cells["E" + (row + 2) + ":F" + (row + 4)].Style.Font.Bold = true;
                    ws.Cells["E" + (row + 4) + ":F" + (row + 4)].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                }


                //Note & Signature
                ws.Cells["B" + (row + 7)].Value = proposal.Note;
                ws.Cells["E" + (row + 9) + ":F" + (row + 9)].Merge = true;
                ws.Cells["E" + (row + 9)].Value = proposal.Signature;
                if (proposal.Signature.Length > 22)
                    ws.Row(row + 9).Height = 32;
                if (proposal.Signature.Length > 44)
                    ws.Row(row + 9).Height = 64;


                //Horizontal alignment for all cells
                ws.Cells["A1:F" + (row + 9)].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                setColumnWidth(ws, 1, 6);
                setColumnWidth(ws, 2, 40);
                setColumnWidth(ws, 3, 7);
                setColumnWidth(ws, 4, 9);
                setColumnWidth(ws, 5, 11);
                setColumnWidth(ws, 6, 14);


                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;  filename=" + fileName + ".xlsx");
                Response.BinaryWrite(pck.GetAsByteArray());
            }
        }

        private void setColumnWidth(ExcelWorksheet ws, int col, int width)
        {
            //ws.Column(col).AutoFit();
            //if (ws.Column(col).Width > width)
            //{
            ws.Column(col).Width = width;
            ws.Column(col).Style.WrapText = true;
            //}
            //else
            //{
            //    ws.Column(col).Width = width;
            //}
        }
    }
}