using Microsoft.AspNetCore.Mvc;
using SporKlubuCodeFirstKatmanliMimariProjectUI.Data.Data;
using SporKulubu.Model;

namespace SporKlubuCodeFirstKatmanlıMimariProjectUI.Controllers
{
    public class SportController : Controller
    {

        private readonly ApplicationDbContext dbcontext;
        public SportController(ApplicationDbContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        public IActionResult Index()
        {
            var result = dbcontext.Sports.ToList();
            return View(result);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Sport sport)
        {

            dbcontext.Sports.Add(sport);
            dbcontext.SaveChanges();
            return RedirectToAction("Index");

        }
        public IActionResult Edit(int id)
        {
            var result = dbcontext.Sports.Find(id);
            return View(result);
        }
        [HttpPost]
        public IActionResult Edit(Sport sport)
        {
            dbcontext.Update(sport);
            dbcontext.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var result = dbcontext.Sports.Find(id);

            if (result != null)
            {
                dbcontext.Sports.Remove(result);
                dbcontext.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
