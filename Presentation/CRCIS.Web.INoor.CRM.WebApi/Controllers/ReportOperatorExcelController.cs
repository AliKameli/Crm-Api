using CRCIS.Web.INoor.CRM.Contract.Repositories.Reports;
using CRCIS.Web.INoor.CRM.Domain.Reports.Operator.Queries;
using CRCIS.Web.INoor.CRM.Utility.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using CRCIS.Web.INoor.CRM.Infrastructure.Authentication.Attributes;

namespace CRCIS.Web.INoor.CRM.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportOperatorExcelController : ControllerBase
    {
        private readonly IReportRepository _reportRepository;
        public ReportOperatorExcelController(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        [JwtAuthorize]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int pageSize,
           [FromQuery] int pageIndex,
           [FromQuery] string sortField,
           [FromQuery] SortOrder? sortOrder,
           [FromQuery] string adminIds,
           [FromQuery] string sourceTypeIds = null,
           [FromQuery] string operationTypeIds = null,
           [FromQuery] string productIds = null,
           [FromQuery] string title = null,
           [FromQuery] string global = null,
           [FromQuery] string range = null,
           [FromQuery] string rangeOperationDate = null)
        {

            pageIndex = 1;
            pageSize = 999999;

            var query = new OperatorReportQuery(pageIndex, pageSize,
             sortField, sortOrder,
             operationTypeIds, sourceTypeIds, productIds, adminIds,
             title, global, range, rangeOperationDate
             );

            var resposne = await _reportRepository.GetOperatorReportAsync(query);

            var stream = new MemoryStream();
            using (var xlPackage = new ExcelPackage(stream))
            {
                var worksheet = xlPackage.Workbook.Worksheets.Add("گزارش اپراتورها");
                var namedStyle = xlPackage.Workbook.Styles.CreateNamedStyle("HyperLink");
                namedStyle.Style.Font.UnderLine = true;
                namedStyle.Style.Font.Color.SetColor(Color.Blue);
                const int startRow = 2;

                //Create Headers and format them


                worksheet.Cells["A1"].Value = "ردیف";
                worksheet.Cells["B1"].Value = "عملیات";
                worksheet.Cells["C1"].Value = "زمان";
                worksheet.Cells["D1"].Value = "نام کاربر";

                worksheet.Cells["E1"].Value = "شناسه مورد";
                worksheet.Cells["F1"].Value = "عنوان";
                worksheet.Cells["G1"].Value = "توضیحات";
                worksheet.Cells["H1"].Value = "تاریخ درج";
                worksheet.Cells["I1"].Value = "محصول";
                worksheet.Cells["J1"].Value = "منشا";
                worksheet.Cells["K1"].Value = "وضعیت";
                worksheet.Cells["L1"].Value = "موضوع";
                worksheet.Cells["M1"].Value = "ایمیل";
                worksheet.Cells["N1"].Value = "موبایل";
                worksheet.Cells["O1"].Value = "نام";
                worksheet.Cells["P1"].Value = "شناسه آی نور";
                worksheet.Cells["A1:P1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells["A1:P1"].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(184, 204, 228));
                worksheet.Cells["A1:P1"].Style.Font.Bold = true;


                worksheet.DefaultColWidth = 25;

                var row = startRow;

                foreach (var item in resposne.Data)
                {
                    worksheet.Cells[row, 1].Value = item.RowNumber;
                    worksheet.Cells[row, 2].Value = item.OperationTypeTitle;
                    worksheet.Cells[row, 3].Value = item.OperationTypeDateTimePersian == null ? "" : item.OperationTypeDateTimePersian;
                    worksheet.Cells[row, 4].Value = item.AdminFullName;

                    worksheet.Cells[row, 5].Value = item.CaseId;
                    worksheet.Cells[row, 6].Value = item.Title;
                    worksheet.Cells[row, 7].Value = item.Description;
                    worksheet.Cells[row, 8].Value = item.CreateDateTimePersian;
                    worksheet.Cells[row, 9].Value = item.ProductTitle;
                    worksheet.Cells[row, 10].Value = item.SourceTypeTitle;
                    worksheet.Cells[row, 11].Value = item.TblName;
                    worksheet.Cells[row, 12].Value = item.FirstSubject;
                    worksheet.Cells[row, 13].Value = item.Email;
                    worksheet.Cells[row, 14].Value = item.Mobile;
                    worksheet.Cells[row, 15].Value = item.NameFamily;
                    worksheet.Cells[row, 16].Value = (item.NoorUserId == null || item.NoorUserId.GetValueOrDefault().Equals(Guid.Empty)) ? "" : item.NoorUserId.ToString();

                    row++;
                }

                // set some core property values
                xlPackage.Workbook.Properties.Title = "Opearatios List";
                xlPackage.Workbook.Properties.Author = "CRM Noor";
                xlPackage.Workbook.Properties.Subject = "OpearatiosList";
                // save the new spreadsheet
                xlPackage.Save();
                // Response.Clear();
            }
            stream.Position = 0;
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"گزارش اپراتور - {range}.xlsx");

        }
    }
}
