using BeFit.Data;
using BeFit.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeFit.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class TypyCwiczenController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TypyCwiczenController(ApplicationDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        public IActionResult Lista()
        {
            var typyCwiczen = _context.TypyÆwiczeñ.ToList();
            return View(typyCwiczen);
        }

        public IActionResult Dodaj() => View();

        [HttpPost]
        public IActionResult Dodaj(TypÆwiczenia model)
        {
            if (ModelState.IsValid)
            {
                _context.TypyÆwiczeñ.Add(model);
                _context.SaveChanges();
                return RedirectToAction(nameof(Lista));
            }
            return View(model);
        }

        public IActionResult Edytuj(int id)
        {
            var typ = _context.TypyÆwiczeñ.Find(id);
            if (typ == null) return NotFound();
            return View(typ);
        }

        [HttpPost]
        public IActionResult Edytuj(TypÆwiczenia model)
        {
            if (ModelState.IsValid)
            {
                _context.TypyÆwiczeñ.Update(model);
                _context.SaveChanges();
                return RedirectToAction(nameof(Lista));
            }
            return View(model);
        }

        public IActionResult Usuñ(int id)
        {
            var typ = _context.TypyÆwiczeñ.Find(id);
            if (typ == null) return NotFound();
            return View(typ);
        }

        [HttpPost, ActionName("Usuñ")]
        public IActionResult PotwierdŸUsuniêcie(int id)
        {
            var typ = _context.TypyÆwiczeñ.Find(id);
            if (typ != null)
            {
                _context.TypyÆwiczeñ.Remove(typ);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Lista));
        }
    }
}
