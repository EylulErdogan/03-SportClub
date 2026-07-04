using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SporKlubuCodeFirstKatmanliMimariProjectUI.Data.Data;
using SporKulubu.Model.viewModel;
namespace SporKlubuCodeFirstKatmanlıMimariProjectUI.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext dbcontext;
        public AdminController(ApplicationDbContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        public IActionResult Index()
        {
            var today = DateTime.Today;

            var model = new AdminDashboardViewModel();

            model.MemberPayments = dbcontext.Members
                .Include(x => x.Sport)
                .Select(x => new MemberCoachPaymentViewModel
                {
                    MemberName = x.FullName,
                    Age = x.Age,
                    SportName = x.Sport.SportName,

                    CoachName = dbcontext.Coaches
                        .Where(c => c.SportId == x.SportId)
                        .Select(c => c.FullName)
                        .FirstOrDefault(),

                    Amount = dbcontext.Payments
                        .Where(p => p.MemberId == x.Id)
                        .Select(p => p.Amount)
                        .FirstOrDefault(),

                    PaymentDate = dbcontext.Payments
                        .Where(p => p.MemberId == x.Id)
                        .Select(p => p.PaymentDate)
                        .FirstOrDefault(),

                    PaymentType = dbcontext.Payments
                        .Where(p => p.MemberId == x.Id)
                        .Select(p => p.PaymentType)
                        .FirstOrDefault(),

                    IsPaid = dbcontext.Payments
                        .Where(p => p.MemberId == x.Id)
                        .Select(p => p.IsPaid)
                        .FirstOrDefault()
                })
                .ToList();

            model.SportCounts = dbcontext.Members
                .Include(x => x.Sport)
                .GroupBy(x => x.Sport.SportName)
                .Select(g => new SportMemberCountViewModel
                {
                    SportName = g.Key,
                    MemberCount = g.Count()
                })
                .ToList();

            model.PaymentStatuses = dbcontext.Payments
                .Include(x => x.Member)
                .Select(x => new PaymentStatusViewModel
                {
                    MemberName = x.Member.FullName,
                    Amount = x.Amount,
                    PaymentDate = x.PaymentDate,
                    PaymentStatus =
                        x.IsPaid ? "Ödendi" :
                        x.PaymentDate < today ? "Geçti" :
                        x.PaymentDate <= today.AddDays(7) ? "Yaklaşıyor" :
                        "Bekleniyor"
                })
                .ToList();

            return View(model);
        }
    }
}
