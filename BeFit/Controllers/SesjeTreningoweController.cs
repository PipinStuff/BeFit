using BeFit.Data;
using BeFit.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeFit.Controllers
{
    [Authorize]
    public class SesjeTreningoweController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SesjeTreningoweController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Lista()
        {
            var idU¿ytkownika = User.Identity.Name;
            var sesje = _context.SesjeTreningowe
                .Where(s => s.IdU¿ytkownika == idU¿ytkownika)
                .ToList();
            return View(sesje);
        }

        public IActionResult Dodaj() => View();

        [HttpPost]
        public IActionResult Dodaj(SesjaTreningowa model)
        {
            if (ModelState.IsValid)
            {
                model.IdU¿ytkownika = User.Identity.Name;
                _context.SesjeTreningowe.Add(model);
                _context.SaveChanges();
                return RedirectToAction(nameof(Lista));
            }
            return View(model);
        }

        public IActionResult Edytuj(int id)
        {
            var sesja = _context.SesjeTreningowe.Find(id);
            if (sesja == null || sesja.IdU¿ytkownika != User.Identity.Name) return Unauthorized();
            return View(sesja);
        }

        [HttpPost]
        public IActionResult Edytuj(SesjaTreningowa model)
        {
            if (ModelState.IsValid)
            {
                _context.SesjeTreningowe.Update(model);
                _context.SaveChanges();
                return RedirectToAction(nameof(Lista));
            }
            return View(model);
        }

        public IActionResult Usuñ(int id)
        {
            var sesja = _context.SesjeTreningowe.Find(id);
            if (sesja == null || sesja.IdU¿ytkownika != User.Identity.Name) return Unauthorized();
            return View(sesja);
        }

        [HttpPost, ActionName("Usuñ")]
        public IActionResult PotwierdŸUsuniêcie(int id)
        {
            var sesja = _context.SesjeTreningowe.Find(id);
            if (sesja != null && sesja.IdU¿ytkownika == User.Identity.Name)
            {
                _context.SesjeTreningowe.Remove(sesja);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Lista));
        }
    }
}
