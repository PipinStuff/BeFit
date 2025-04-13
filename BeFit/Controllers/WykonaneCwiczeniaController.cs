using BeFit.Data;
using BeFit.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

[Authorize]
public class WykonaneCwiczeniaController : Controller
{
    private readonly ApplicationDbContext _kontekst;
    private readonly UserManager<Uøytkownik> _menedøerUøytkownika;

    public WykonaneCwiczeniaController(ApplicationDbContext kontekst, UserManager<Uøytkownik> menedøerUøytkownika)
    {
        _kontekst = kontekst;
        _menedøerUøytkownika = menedøerUøytkownika;
    }

    public IActionResult Lista(int id)
    {
        var Êwiczenia = _kontekst.Wykonane∆wiczenia
            .Where(c => c.IdSesjiTreningowej == id)
            .Include(c => c.Typ∆wiczenia)
            .ToList();

        ViewBag.SesjaId = id;
        return View(Êwiczenia);
    }


    public IActionResult Dodaj()
    {
        var idUøytkownika = _menedøerUøytkownika.GetUserId(User);
        ViewBag.Typy∆wiczeÒ = new SelectList(_kontekst.Typy∆wiczeÒ, "Id", "Nazwa");
        ViewBag.SesjeTreningowe = new SelectList(_kontekst.SesjeTreningowe.Where(s => s.IdUøytkownika == idUøytkownika), "Id", "DataRozpoczÍcia");
        return View();
    }


    [HttpPost]
    public IActionResult Dodaj(int id,Wykonane∆wiczenie model)
    {
        var idUøytkownika = _menedøerUøytkownika.GetUserId(User);
        model.IdSesjiTreningowej = id;
        var sesja = _kontekst.SesjeTreningowe
            .FirstOrDefault(s => s.Id == model.IdSesjiTreningowej);

        if (sesja == null || sesja.IdUøytkownika != idUøytkownika)
        {
            return Unauthorized();
        }

        var typCwiczenia = _kontekst.Typy∆wiczeÒ.FirstOrDefault(t => t.Id == model.IdTypu∆wiczenia);
        if (typCwiczenia == null)
        {
            ModelState.AddModelError(nameof(model.IdTypu∆wiczenia), "Wybrany typ Êwiczenia nie istnieje.");
            ViewBag.Typy∆wiczeÒ = new SelectList(_kontekst.Typy∆wiczeÒ, "Id", "Nazwa");
            return View(model);
        }

        model.SesjaTreningowa = sesja;
        model.Typ∆wiczenia = typCwiczenia;
        model=new Wykonane∆wiczenie
        {
            IdSesjiTreningowej = model.IdSesjiTreningowej,
            IdTypu∆wiczenia = model.IdTypu∆wiczenia,
            Obciπøenie = model.Obciπøenie,
            LiczbaSerii = model.LiczbaSerii,
            PowtÛrzenia = model.PowtÛrzenia,
            SesjaTreningowa=model.SesjaTreningowa,
            Typ∆wiczenia = model.Typ∆wiczenia
        };
        _kontekst.Wykonane∆wiczenia.Add(model);
        _kontekst.SaveChanges();

        return RedirectToAction(nameof(Lista), new { id = model.IdSesjiTreningowej });
    }

    public IActionResult Edytuj(int id)
    {
        var Êwiczenie = _kontekst.Wykonane∆wiczenia
    .Include(c => c.SesjaTreningowa)
    .Include(c => c.Typ∆wiczenia)
    .FirstOrDefault(c => c.Id == id);
        if (Êwiczenie == null || Êwiczenie.SesjaTreningowa.IdUøytkownika != _menedøerUøytkownika.GetUserId(User)) return Unauthorized();
        ViewBag.Typy∆wiczeÒ = new SelectList(_kontekst.Typy∆wiczeÒ, "Id", "Nazwa", Êwiczenie.IdTypu∆wiczenia);
        return View(Êwiczenie);
    }

    [HttpPost]
    public IActionResult Edytuj(Wykonane∆wiczenie model)
    {
        model.Typ∆wiczenia = _kontekst.Typy∆wiczeÒ.FirstOrDefault(t => t.Id == model.IdTypu∆wiczenia);
        model.SesjaTreningowa=  _kontekst.SesjeTreningowe.FirstOrDefault(s => s.Id == model.IdSesjiTreningowej);
        _kontekst.Wykonane∆wiczenia.Update(model);
            _kontekst.SaveChanges();
        return RedirectToAction(nameof(Lista), new { id = model.IdSesjiTreningowej });

    }

    public IActionResult UsuÒ(int id)
    {
        var Êwiczenie = _kontekst.Wykonane∆wiczenia
    .Include(c => c.SesjaTreningowa)
    .Include(c => c.Typ∆wiczenia)
    .FirstOrDefault(c => c.Id == id);
        if (Êwiczenie == null || Êwiczenie.SesjaTreningowa.IdUøytkownika != _menedøerUøytkownika.GetUserId(User)) return Unauthorized();
        return View(Êwiczenie);
    }

    [HttpPost, ActionName("PotwierdzUsuniecie")]
    public IActionResult PotwierdzUsuniecie(int id)
    {
        var Êwiczenie = _kontekst.Wykonane∆wiczenia
    .Include(c => c.SesjaTreningowa)
    .Include(c => c.Typ∆wiczenia)
    .FirstOrDefault(c => c.Id == id);
        if (Êwiczenie != null && Êwiczenie.SesjaTreningowa.IdUøytkownika == _menedøerUøytkownika.GetUserId(User))
        {
            _kontekst.Wykonane∆wiczenia.Remove(Êwiczenie);
            _kontekst.SaveChanges();
        }
        return RedirectToAction(nameof(Lista), new { id = Êwiczenie.IdSesjiTreningowej });
    }
}
