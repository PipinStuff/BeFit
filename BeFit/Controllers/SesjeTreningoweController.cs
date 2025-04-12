using BeFit.Data;
using BeFit.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
            if (string.IsNullOrEmpty(model.IdU¿ytkownika))
            {
                ModelState.AddModelError(nameof(model.IdU¿ytkownika), "IdU¿ytkownika jest wymagane.");
            }
            if (ModelState.IsValid)
            {
                _context.SesjeTreningowe.Add(model);
                _context.SaveChanges();
                return RedirectToAction(nameof(Lista));
            }
            else
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }
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
