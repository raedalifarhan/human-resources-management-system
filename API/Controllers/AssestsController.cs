using Microsoft.AspNetCore.Mvc;
using Application.Assests;
using Microsoft.AspNetCore.Authorization;
using Persistence;
using Microsoft.EntityFrameworkCore;
using API.DTOs;
using System.Data;
using ClosedXML.Excel;

namespace API.Controllers
{
    [AllowAnonymous]
    public class AssestsController : BaseApiController
    {
        private readonly DataContext _context;
        public AssestsController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(UploadFileDto model)
        {
            return HandleResult(await Mediator!.Send(new Application.Assests.Create.Command { model = model }));
        }

        [HttpGet("{fileName}")]
        public async Task<IActionResult> DownloadFile(string fileName)
        {
            var filePath = Path.Combine("wwwroot", "files", fileName);

            if (System.IO.File.Exists(filePath))
            {
                var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
                return File(fileBytes, "application/octet-stream", fileName);
            }

            return NotFound("File Not Found");
        }

        [HttpGet]
        public async Task<IActionResult> ExportCompanyAsExcelSheet()
        {
            using (var workBook = new XLWorkbook())
            {
                var workSheet = workBook.Worksheets.Add("companies");
                
                var row = 1;

                workSheet.Cell(row, 1).Value = "المعرف داتا البيانات";
                workSheet.Cell(row, 2).Value = "الكود";
                workSheet.Cell(row, 3).Value = "اسم الشركة";
                workSheet.Cell(row, 4).Value = "رأس مال الشركة";
                workSheet.Cell(row, 5).Value = "حالة الرخصة";
                workSheet.Cell(row, 6).Value = "اسم الشركة القديم";
                workSheet.Cell(row, 7).Value = "العنوان";
                workSheet.Cell(row, 8).Value = "نمط النشاط";
                workSheet.Cell(row, 9).Value = "رقم السجل التجاري.";
                workSheet.Cell(row, 10).Value = "العقوبات";
                workSheet.Cell(row, 11).Value = "مسؤول الامتثال";
                workSheet.Cell(row, 12).Value = "تاريخ الانشاء في النظام";
                workSheet.Cell(row, 13).Value = "نمط الشركة مكتب/شركة";
                workSheet.Cell(row, 14).Value = "معلومات اضافية";
                workSheet.Cell(row, 15).Value = "رقم الهاتف";
                workSheet.Cell(row, 15).Value = "رقم مسؤول الامتثال";

                workSheet.Cell(row, 17).Value = "الضمانة المالية";
                workSheet.Cell(row, 18).Value = "تاريخ الموافقة المبدئية";
                workSheet.Cell(row, 19).Value = "تاريخ التقديم";
                workSheet.Cell(row, 20).Value = "رسزم الرخصة";
                workSheet.Cell(row, 21).Value = "رقم الرخصة";
                workSheet.Cell(row, 22).Value = "حالة طلب الترخيص";
                workSheet.Cell(row, 23).Value = "ملاحظات طلب الترخيص";
                workSheet.Cell(row, 24).Value = "رسوم الطلب";

                foreach (var comp in await GetCompaniesWithLatestLicence())
                {
                    row++;
                    workSheet.Cell(row, 1).Value =  comp?.Id.ToString();
                    workSheet.Cell(row, 2).Value =  comp?.Code.ToString();
                    workSheet.Cell(row, 3).Value =  comp?.CompanyName.ToString();
                    workSheet.Cell(row, 4).Value =  comp?.CompanyCapital.ToString();
                    workSheet.Cell(row, 5).Value =  comp?.LicenceStatus.ToString();
                    workSheet.Cell(row, 6).Value =  comp?.OldComericalName?.ToString();
                    workSheet.Cell(row, 7).Value =  comp?.Address?.ToString();
                    workSheet.Cell(row, 8).Value =  comp?.TypeOfActivity?.ToString();
                    workSheet.Cell(row, 9).Value =  comp?.CommercialRegistrationNo.ToString();
                    workSheet.Cell(row, 10).Value = comp?.ViolationsAndPenalties?.ToString();
                    workSheet.Cell(row, 11).Value = comp?.ComplianceOfficer?.ToString();
                    workSheet.Cell(row, 12).Value = comp?.CreateDate.ToString();
                    workSheet.Cell(row, 13).Value = comp?.CompanyType.ToString();
                    workSheet.Cell(row, 14).Value = comp?.Info?.ToString();
                    workSheet.Cell(row, 15).Value = comp?.PhoneNumber?.ToString();
                    workSheet.Cell(row, 16).Value = comp?.ComplianceOfficerPhone?.ToString();

                    workSheet.Cell(row, 17).Value = comp?.LatestLicence?.FinancialGuarantee?.ToString();
                    workSheet.Cell(row, 18).Value = comp?.LatestLicence?.DateOfPreliminaryApproval?.ToString();
                    workSheet.Cell(row, 19).Value = comp?.LatestLicence?.DateOfApplication?.ToString();
                    workSheet.Cell(row, 20).Value = comp?.LatestLicence?.LicenceFee?.ToString();
                    workSheet.Cell(row, 21).Value = comp?.LatestLicence?.LicenceNo?.ToString();
                    workSheet.Cell(row, 22).Value = comp?.LatestLicence?.LicenceRequestStatus?.ToString();
                    workSheet.Cell(row, 23).Value = comp?.LatestLicence?.Notes?.ToString();
                    workSheet.Cell(row, 24).Value = comp?.LatestLicence?.ApplicationFee?.ToString();

                }

                using (var stream = new MemoryStream())
                {
                    workBook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(content, "application/vnd.openxmlforamts-officedocuments.spreadsheetml.sheet", "companies.xlsx");
                }
            }
        }

        private async Task<List<CompanyLatestLicenceDetails>> GetCompaniesWithLatestLicence()
        {
            var companiesWithLatestLicence = await _context.Companies
                .Include(company => company.LicenceDetails)
                .Select(company => new CompanyLatestLicenceDetails
                {
                    Id = company.Id,
                    Code = company.Code,
                    CompanyName = company!.CompanyName,
                    CompanyCapital = company!.CompanyCapital,
                    LicenceStatus = company!.LicenceStatus,
                    OldComericalName = company.OldComericalName,
                    Address = company!.Address,
                    TypeOfActivity = company!.TypeOfActivity,
                    CommercialRegistrationNo = company!.CommercialRegistrationNo,
                    ViolationsAndPenalties = company.ViolationsAndPenalties,
                    ComplianceOfficer = company.ComplianceOfficer,
                    ComplianceOfficerPhone = company.ComplianceOfficerPhone,
                    CreateDate = company!.CreateDate,
                    CompanyType = company!.CompanyType,
                    Info = company.Info,
                    PhoneNumber = company!.PhoneNumber,
                    LatestLicence = company!.LicenceDetails!
                        .OrderByDescending(licence => licence.CreateDate)
                        .FirstOrDefault()!
                })
                .ToListAsync();

            return companiesWithLatestLicence;
        }
    }
}
