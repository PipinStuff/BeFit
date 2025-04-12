using BeFit.Data;
using BeFit.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BeFit.Controllers
{
    [Authorize]
    public class SesjeTreningoweController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<U¿ytkownik> _userManager;

        public SesjeTreningoweController(ApplicationDbContext context, UserManager<U¿ytkownik> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Lista()
        {
            var idU¿ytkownika = _userManager.GetUserId(User);
            var sesje = _context.SesjeTreningowe
                .Where(s => s.IdU¿ytkownika == idU¿ytkownika)
                .ToList();
            return View(sesje);
        }

        public IActionResult Dodaj() => View();

        [HttpPost]
        public IActionResult Dodaj(SesjaTreningowa model)
        {
            model.IdU¿ytkownika = _userManager.GetUserId(User);
            if (model.DataZakoñczenia < model.DataRozpoczêcia)
            {
                ModelState.AddModelError(nameof(model.DataZakoñczenia), "Data zakoñczenia musi byæ póŸniejsza ni¿ data rozpoczêcia.");
            }
            else if (model.DataZakoñczenia< new DateTime(2000, 1, 1) || model.DataRozpoczêcia< new DateTime(2000, 1, 1)) 
            {
                ModelState.AddModelError(nameof(model.DataZakoñczenia), "Daty nie mog¹ pozostaæ puste lub byæ zbyt odleg³e");
            }
            else
            {
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
        public IActionResult Æwiczenia(int id)
        {
            var æwiczenia = _context.WykonaneÆwiczenia
                .Where(c => c.IdSesjiTreningowej == id)
                .Include(c => c.TypÆwiczenia)
                .ToList();

            ViewBag.SesjaId = id;
            return View(æwiczenia);
        }

    }
}
