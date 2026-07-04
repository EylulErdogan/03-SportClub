using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SporKlubuCodeFirstKatmanliMimariProjectUI.Data.Data;
using SporKulubu.Model;

namespace SporKlubuCodeFirstKatmanlıMimariProjectUI.Controllers
{
    public class CoachController : Controller
    {

        private readonly ApplicationDbContext dbcontext;
        public CoachController(ApplicationDbContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        public IActionResult Index()
        {
            var result = dbcontext.Coaches
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
        public IActionResult Create(Coach coach)
        {
            dbcontext.Coaches.Add(coach);
            dbcontext.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var result = dbcontext.Coaches.Find(id);

            ViewBag.SportId = new SelectList(
                dbcontext.Sports,
                "Id",
                "SportName",
                result.SportId
            );

            return View(result);
        }

        [HttpPost]
        public IActionResult Edit(Coach coach)
        {
            dbcontext.Coaches.Update(coach);
            dbcontext.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var result = dbcontext.Coaches.Find(id);

            if (result != null)
            {
                dbcontext.Coaches.Remove(result);
                dbcontext.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
