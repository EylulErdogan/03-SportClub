using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SporKlubuCodeFirstKatmanliMimariProjectUI.Data.Data;

namespace SporKlubuCodeFirstKatmanlıMimariProjectUI.Controllers
{
    public class ReportController : Controller
    {
        private readonly ApplicationDbContext db;

        public ReportController(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IActionResult LastMembers()
        {
            var result = db.Members
                .OrderByDescending(x => x.Id)
                .Take(10)
                .ToList();

            return View(result);
        }

        public IActionResult ThisMonthPaidMembers()
        {
            var today = DateTime.Today;

            var result = db.Payments
                .Include(x => x.Member)
                .Where(x => x.IsPaid &&
                            x.PaymentDate.Month == today.Month &&
                            x.PaymentDate.Year == today.Year)
                .OrderByDescending(x => x.PaymentDate)
                .ToList();

            return View(result);
        }

        public IActionResult MembersBySport()
        {
            var result = db.Members
                .Include(x => x.Sport)
                .OrderBy(x => x.Sport.SportName)
                .ToList();

            return View(result);
        }
    }
}
