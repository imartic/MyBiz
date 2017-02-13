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
                ws.Cells["A1"].Value = proposal.CompanyName;
                ws.Cells["A2"].Value = proposal.CompanyAddress;
                ws.Cells["A3"].Value = proposal.CompanyCity;
                ws.Cells["A4"].Value = proposal.CompanyPIN.ToString();
                ws.Cells["D1"].Value = proposal.CompanyPhone;
                ws.Cells["D2"].Value = proposal.CompanyFax;
                ws.Cells["D3"].Value = proposal.CompanyEmail;
                ws.Cells["D4"].Value = proposal.CompanyIBAN;

                ws.Cells["A1:B1"].Merge = true;
                ws.Cells["A2:B2"].Merge = true;
                ws.Cells["A3:B3"].Merge = true;
                ws.Cells["A4:B4"].Merge = true;

                ws.Cells["D1:F1"].Merge = true;
                ws.Cells["D2:F2"].Merge = true;
                ws.Cells["D3:F3"].Merge = true;
                ws.Cells["D4:F4"].Merge = true;

                ws.Cells["A4:F4"].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                using (ExcelRange rng = ws.Cells["D1:F4"])
                {
                    rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                }

                using (ExcelRange rng = ws.Cells["A1:D4"])
                {
                    rng.Style.Font.Bold = true;
                }


                //Client
                ws.Cells["E9"].Value = proposal.ClientName;
                ws.Cells["E10"].Value = proposal.ClientAddress;
                ws.Cells["E11"].Value = proposal.ClientCity;
                ws.Cells["E12"].Value = proposal.ClientPhone;
                ws.Cells["E13"].Value = proposal.ClientEmail;
                ws.Cells["E14"].Value = proposal.ClientPIN;

                ws.Cells["E9:F9"].Merge = true;
                ws.Cells["E10:F10"].Merge = true;
                ws.Cells["E11:F11"].Merge = true;
                ws.Cells["E12:F12"].Merge = true;
                ws.Cells["E13:F13"].Merge = true;
                ws.Cells["E14:F14"].Merge = true;

                ws.Cells["E9:F9"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                ws.Cells["F9:F14"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                ws.Cells["E14:F14"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                ws.Cells["E9:F14"].Style.Border.Left.Style = ExcelBorderStyle.Thin;


                //Items title
                ws.Cells["A18:F18"].Merge = true;
                ws.Cells["A18"].Value = proposal.ItemsTitle;
                ws.Cells["A18"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells["A18"].Style.Font.UnderLine = true;

                ws.Cells["A21"].Value = "BR."; //todo: jezik proslijediti iz postavki...
                ws.Cells["B21"].Value = "NAZIV ARTIKLA/USLUGE";
                ws.Cells["C21"].Value = "J. MJ.";
                ws.Cells["D21"].Value = "KOL.";
                ws.Cells["E21"].Value = "CIJENA";
                ws.Cells["F21"].Value = "VRIJEDNOST";

                ws.Cells["A21:F21"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                //Items
                var items = DbItems.LoadAll(proposalId);
                var row = 22;
                if (items != null && items.Count > 0)
                {
                    for(int i = 0; i < items.Count; i++)
                    {
                        ws.Cells["A" + row].Value = items[i].ItemNumber;
                        ws.Cells["B" + row].Value = items[i].ItemText;
                        ws.Cells["C" + row].Value = items[i].Unit;
                        ws.Cells["D" + row].Value = items[i].Quantity;
                        ws.Cells["E" + row].Value = items[i].UnitPrice;
                        ws.Cells["F" + row].Value = items[i].TotalPrice;

                        row++;
                    }
                    ws.Cells["D" + 22 + ":F" + row].Style.Numberformat.Format = "0.00";

                    ws.Cells["A" + (row - 1) + ":F" + (row - 1)].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;


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

                    using (ExcelRange rng = ws.Cells["E" + (row + 2) + ":F" + (row + 4)])
                    {
                        rng.Style.Font.Bold = true;
                    }
                }


                //Note & Signature
                ws.Cells["B" + (row + 6)].Value = proposal.Note;
                ws.Cells["E" + (row + 8)].Value = proposal.Signature;
                ws.Cells["E" + (row + 8)].Style.WrapText = true;


                //Horizontal alignment for all cells
                ws.Cells["A1:F" + (row + 8)].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                ws.Column(1).Width = 6;
                if (ws.Column(1).Width > 6)
                {
                    ws.Column(1).Width = 6;
                    ws.Column(1).Style.WrapText = true;
                }
                ws.Column(2).Width = 42;
                if (ws.Column(2).Width > 42)
                {
                    ws.Column(2).Width = 42;
                    ws.Column(2).Style.WrapText = true;
                }
                ws.Column(3).Width = 7;
                if (ws.Column(3).Width > 7)
                {
                    ws.Column(3).Width = 7;
                    ws.Column(3).Style.WrapText = true;
                }
                ws.Column(4).Width = 9;
                if (ws.Column(4).Width > 9)
                {
                    ws.Column(4).Width = 9;
                    ws.Column(4).Style.WrapText = true;
                }
                ws.Column(5).Width = 12;
                if (ws.Column(5).Width > 12)
                {
                    ws.Column(5).Width = 12;
                    ws.Column(5).Style.WrapText = true;
                }
                ws.Column(6).Width = 12;
                if (ws.Column(6).Width > 12)
                {
                    ws.Column(6).Width = 12;
                    ws.Column(6).Style.WrapText = true;
                }


                //todo: fit to a4 page...
                //ws.PrinterSettings.FitToPage = true;


                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;  filename=" + fileName + ".xlsx");
                Response.BinaryWrite(pck.GetAsByteArray());
            }
        }
    }
}