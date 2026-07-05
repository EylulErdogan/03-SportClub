using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SporKlubuCodeFirstKatmanliMimariProjectUI.Data.Data;
using SporKulubu.Model.viewModel;

namespace SporKulubuCodeFirstKatmanliMimariProjectUI.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var model = new AdminDashboardViewModel();

            model.MemberCount = _context.Members.Count();
            model.CoachCount = _context.Coaches.Count();
            model.SportCount = _context.Sports.Count();

            model.TotalPaymentAmount = _context.Payments.Sum(x => x.Amount);

            model.PaidAmount = _context.Payments
                .Where(x => x.IsPaid)
                .Sum(x => x.Amount);

            model.UnpaidAmount = _context.Payments
                .Where(x => !x.IsPaid)
                .Sum(x => x.Amount);

            model.MemberPayments = _context.Payments
                .Include(x => x.Member)
                .OrderByDescending(x => x.PaymentDate)
                .Select(x => new PaymentStatusViewModel
                {
                    MemberName = x.Member.FullName,
                    Amount = x.Amount,
                    PaymentDate = x.PaymentDate,
                    IsPaid = x.IsPaid
                })
                .Take(5)
                .ToList();

            model.SportCounts = _context.Members
                .Include(x => x.Sport)
                .GroupBy(x => x.Sport.SportName)
                .Select(x => new SportMemberCountViewModel
                {
                    SportName = x.Key,
                    MemberCount = x.Count()
                })
                .ToList();

            model.CoachReports = _context.Coaches
                .Include(x => x.Sport)
                .Select(x => new MemberCoachPaymentViewModel
                {
                    CoachName = x.FullName,
                    SportName = x.Sport.SportName,
                    Salary = x.Salary
                })
                .Take(5)
                .ToList();

            return View(model);
        }
    }
}