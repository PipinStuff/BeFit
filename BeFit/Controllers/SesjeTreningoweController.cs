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

        private readonly UserManager<U�ytkownik> _userManager;

        public SesjeTreningoweController(ApplicationDbContext context, UserManager<U�ytkownik> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Lista()
        {
            var idU�ytkownika = _userManager.GetUserId(User);
            var sesje = _context.SesjeTreningowe
                .Where(s => s.IdU�ytkownika == idU�ytkownika)
                .ToList();
            return View(sesje);
        }

        public IActionResult Dodaj() => View();

        [HttpPost]
        public IActionResult Dodaj(SesjaTreningowa model)
        {
            model.IdU�ytkownika = _userManager.GetUserId(User);
            if (model.DataZako�czenia < model.DataRozpocz�cia)
            {
                ModelState.AddModelError(nameof(model.DataZako�czenia), "Data zako�czenia musi by� p�niejsza ni� data rozpocz�cia.");
            }
            else if (model.DataZako�czenia< new DateTime(2000, 1, 1) || model.DataRozpocz�cia< new DateTime(2000, 1, 1)) 
            {
                ModelState.AddModelError(nameof(model.DataZako�czenia), "Daty nie mog� pozosta� puste lub by� zbyt odleg�e");
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
            if (sesja == null || sesja.IdU�ytkownika != User.Identity.Name) return Unauthorized();
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

        public IActionResult Usu�(int id)
        {
            var sesja = _context.SesjeTreningowe.Find(id);
            if (sesja == null || sesja.IdU�ytkownika != User.Identity.Name) return Unauthorized();
            return View(sesja);
        }

        [HttpPost, ActionName("Usu�")]
        public IActionResult Potwierd�Usuni�cie(int id)
        {
            var sesja = _context.SesjeTreningowe.Find(id);
            if (sesja != null && sesja.IdU�ytkownika == User.Identity.Name)
            {
                _context.SesjeTreningowe.Remove(sesja);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Lista));
        }
        public IActionResult �wiczenia(int id)
        {
            var �wiczenia = _context.Wykonane�wiczenia
                .Where(c => c.IdSesjiTreningowej == id)
                .Include(c => c.Typ�wiczenia)
                .ToList();

            ViewBag.SesjaId = id;
            return View(�wiczenia);
        }

    }
}
