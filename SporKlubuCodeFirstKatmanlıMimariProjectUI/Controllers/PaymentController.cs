using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SporKlubuCodeFirstKatmanliMimariProjectUI.Data.Data;
using SporKulubu.Model;

namespace SporKlubuCodeFirstKatmanlıMimariProjectUI.Controllers
{
   
        public class PaymentController : Controller
        {
            private readonly ApplicationDbContext dbcontext;

            public PaymentController(ApplicationDbContext dbcontext)
            {
                this.dbcontext = dbcontext;
            }

            public IActionResult Index()
            {
                var result = dbcontext.Payments
                    .Include(x => x.Member)
                    .ToList();

                return View(result);
            }

            [HttpGet]
            public IActionResult Create()
            {
                ViewBag.MemberId = new SelectList(dbcontext.Members, "Id", "FullName");
                return View();
            }

            [HttpPost]
            public IActionResult Create(Payment payment)
            {
                dbcontext.Payments.Add(payment);
                dbcontext.SaveChanges();

                return RedirectToAction("Index");
            }

            [HttpGet]
            public IActionResult Edit(int id)
            {
                var result = dbcontext.Payments.Find(id);

                ViewBag.MemberId = new SelectList(
                    dbcontext.Members,
                    "Id",
                    "FullName",
                    result.MemberId
                );

                return View(result);
            }

            [HttpPost]
            public IActionResult Edit(Payment payment)
            {
                dbcontext.Payments.Update(payment);
                dbcontext.SaveChanges();

                return RedirectToAction("Index");
            }

            public IActionResult Delete(int id)
            {
                var result = dbcontext.Payments.Find(id);

                dbcontext.Payments.Remove(result);
                dbcontext.SaveChanges();

                return RedirectToAction("Index");
            }
        }
    }

