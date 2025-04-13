using BeFit.Data;
using BeFit.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace BeFit.Controllers
{
    [Authorize]
    public class StatystykiController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Użytkownik> _userManager;

        public StatystykiController(ApplicationDbContext context, UserManager<Użytkownik> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Statystyki()
        {
            var idUżytkownika = _userManager.GetUserId(User);
            var czteryTygodnieTemu = DateTime.Now.AddDays(-28);

            var statystyki = _context.WykonaneĆwiczenia
                .Include(w => w.TypĆwiczenia)
                .Include(w => w.SesjaTreningowa)
                .Where(w => w.SesjaTreningowa.IdUżytkownika == idUżytkownika &&
                            w.SesjaTreningowa.DataRozpoczęcia >= czteryTygodnieTemu)
                .GroupBy(w => w.IdTypuĆwiczenia)
                .Select(g => new Statystyki
                {
                    TypĆwiczenia = g.First().TypĆwiczenia.Nazwa,
                    LiczbaWykonań = g.Count(),
                    ŁącznaLiczbaPowtórzeń = g.Sum(w => w.LiczbaSerii * w.Powtórzenia),
                    ŚrednieObciążenie = g.Average(w => w.Obciążenie),
                    MaksymalneObciążenie = g.Max(w => w.Obciążenie)
                })
                .ToList();

            return View(statystyki);
        }
    }
}
