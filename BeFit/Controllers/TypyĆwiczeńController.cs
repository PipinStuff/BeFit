using BeFit.Data;
using BeFit.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeFit.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class Typ�wiczeniaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public Typ�wiczeniaController(ApplicationDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        public IActionResult Lista()
        {
            var typy�wicze� = _context.Typy�wicze�.ToList();
            return View(typy�wicze�);
        }

        public IActionResult Dodaj() => View();

        [HttpPost]
        public IActionResult Dodaj(Typ�wiczenia model)
        {
            if (ModelState.IsValid)
            {
                _context.Typy�wicze�.Add(model);
                _context.SaveChanges();
                return RedirectToAction(nameof(Lista));
            }
            return View(model);
        }

        public IActionResult Edytuj(int id)
        {
            var typ = _context.Typy�wicze�.Find(id);
            if (typ == null) return NotFound();
            return View(typ);
        }

        [HttpPost]
        public IActionResult Edytuj(Typ�wiczenia model)
        {
            if (ModelState.IsValid)
            {
                _context.Typy�wicze�.Update(model);
                _context.SaveChanges();
                return RedirectToAction(nameof(Lista));
            }
            return View(model);
        }

        public IActionResult Usu�(int id)
        {
            var typ = _context.Typy�wicze�.Find(id);
            if (typ == null) return NotFound();
            return View(typ);
        }

        [HttpPost, ActionName("Usu�")]
        public IActionResult Potwierd�Usuni�cie(int id)
        {
            var typ = _context.Typy�wicze�.Find(id);
            if (typ != null)
            {
                _context.Typy�wicze�.Remove(typ);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Lista));
        }
    }
}
