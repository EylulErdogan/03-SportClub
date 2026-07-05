using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using SporKlubuCodeFirstKatmanliMimariProjectUI.Data.Data;
using SporKulubu.Model;

namespace SporKlubuCodeFirstKatmanlıMimariProjectUI.Controllers
{
    public class MemberController : Controller
    {

        private readonly ApplicationDbContext dbcontext;
        public MemberController(ApplicationDbContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        public IActionResult Index()
        {
            var result = dbcontext.Members
                                  .Include(x => x.Sport)
                                  .ToList();

            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.SportId = new SelectList(dbcontext.Sports, "Id", "SportName");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Member member)
        {
            dbcontext.Members.Add(member);
            dbcontext.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            var result = dbcontext.Members.Find(id);

            ViewBag.SportId = new SelectList(
                dbcontext.Sports,
                "Id",
                "SportName",
                result.SportId
            );

            return View(result);
        }

        [HttpPost]
        public IActionResult Edit(Member member)
        {
            dbcontext.Members.Update(member);
            dbcontext.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var result = dbcontext.Members.Find(id);

            if (result != null)
            {
                dbcontext.Members.Remove(result);
                dbcontext.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public IActionResult ExportToPdf()
        {
            QuestPDF.Settings.License = LicenseType.Community;

            var members = dbcontext.Members
                .Include(x => x.Sport)
                .ToList();

            var pdfDocument = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(11).FontFamily("Arial"));

                    page.Header()
                        .Text("Üye Listesi Raporu")
                        .SemiBold()
                        .FontSize(20)
                        .FontColor(Colors.Blue.Medium);

                    page.Content()
                        .PaddingTop(1, Unit.Centimetre)
                        .Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.ConstantColumn(40);
                                columns.RelativeColumn();
                                columns.ConstantColumn(50);
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                            });

                            table.Header(header =>
                            {
                                header.Cell().Background(Colors.Grey.Lighten2).Padding(5).Text("ID").Bold();
                                header.Cell().Background(Colors.Grey.Lighten2).Padding(5).Text("Ad Soyad").Bold();
                                header.Cell().Background(Colors.Grey.Lighten2).Padding(5).Text("Yaş").Bold();
                                header.Cell().Background(Colors.Grey.Lighten2).Padding(5).Text("Telefon").Bold();
                                header.Cell().Background(Colors.Grey.Lighten2).Padding(5).Text("Email").Bold();
                                header.Cell().Background(Colors.Grey.Lighten2).Padding(5).Text("Spor").Bold();
                            });

                            foreach (var item in members)
                            {
                                table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten3).Padding(5).Text(item.Id.ToString());
                                table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten3).Padding(5).Text(item.FullName);
                                table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten3).Padding(5).Text(item.Age.ToString());
                                table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten3).Padding(5).Text(item.Phone);
                                table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten3).Padding(5).Text(item.Email);
                                table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten3).Padding(5).Text(item.Sport.SportName);
                            }
                        });

                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("Sayfa ");
                            x.CurrentPageNumber();
                        });
                });
            });

            var pdfBytes = pdfDocument.GeneratePdf();

            return File(pdfBytes, "application/pdf", $"Uye_Listesi_{DateTime.Now:yyyyMMdd}.pdf");
        }
        public IActionResult ExportToExcel()
        {
            ExcelPackage.License.SetNonCommercialPersonal("Backend softito");

            var members = dbcontext.Members
                .Include(x => x.Sport)
                .ToList();

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Üye Listesi");

                worksheet.Cells[1, 1].Value = "ID";
                worksheet.Cells[1, 2].Value = "Ad Soyad";
                worksheet.Cells[1, 3].Value = "Yaş";
                worksheet.Cells[1, 4].Value = "Telefon";
                worksheet.Cells[1, 5].Value = "Email";
                worksheet.Cells[1, 6].Value = "Spor Dalı";

                using (var range = worksheet.Cells[1, 1, 1, 6])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(41, 128, 185));
                    range.Style.Font.Color.SetColor(System.Drawing.Color.White);
                    range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                }

                int rowNumber = 2;

                foreach (var item in members)
                {
                    worksheet.Cells[rowNumber, 1].Value = item.Id;
                    worksheet.Cells[rowNumber, 2].Value = item.FullName;
                    worksheet.Cells[rowNumber, 3].Value = item.Age;
                    worksheet.Cells[rowNumber, 4].Value = item.Phone;
                    worksheet.Cells[rowNumber, 5].Value = item.Email;
                    worksheet.Cells[rowNumber, 6].Value = item.Sport.SportName;

                    rowNumber++;
                }

                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                var fileBytes = package.GetAsByteArray();

                return File(
                    fileBytes,
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    $"Uye_Listesi_{DateTime.Now:yyyyMMdd}.xlsx"
                );
            }
        }

    }
}
