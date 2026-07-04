using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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

    }
}
