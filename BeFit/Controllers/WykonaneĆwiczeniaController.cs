using BeFit.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BeFit.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

[Authorize]
public class Wykonane∆wiczeniaController : Controller
{
    private readonly ApplicationDbContext _kontekst;
    private readonly UserManager<Uøytkownik> _menedøerUøytkownika;

    public Wykonane∆wiczeniaController(ApplicationDbContext kontekst, UserManager<Uøytkownik> menedøerUøytkownika)
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
    public IActionResult Dodaj(Wykonane∆wiczenie model)
    {
        if (ModelState.IsValid)
        {
            var idUøytkownika = _menedøerUøytkownika.GetUserId(User);
            var sesja = _kontekst.SesjeTreningowe.Find(model.IdSesjiTreningowej);

            if (sesja == null || sesja.IdUøytkownika != idUøytkownika) return Unauthorized();

            _kontekst.Wykonane∆wiczenia.Add(model);
            _kontekst.SaveChanges();
            return RedirectToAction(nameof(Lista));
        }
        return View(model);
    }

    public IActionResult Edytuj(int id)
    {
        var Êwiczenie = _kontekst.Wykonane∆wiczenia.Find(id);
        if (Êwiczenie == null || Êwiczenie.SesjaTreningowa.IdUøytkownika != _menedøerUøytkownika.GetUserId(User)) return Unauthorized();
        return View(Êwiczenie);
    }

    [HttpPost]
    public IActionResult Edytuj(Wykonane∆wiczenie model)
    {
        if (ModelState.IsValid)
        {
            _kontekst.Wykonane∆wiczenia.Update(model);
            _kontekst.SaveChanges();
            return RedirectToAction(nameof(Lista));
        }
        return View(model);
    }

    public IActionResult UsuÒ(int id)
    {
        var Êwiczenie = _kontekst.Wykonane∆wiczenia.Find(id);
        if (Êwiczenie == null || Êwiczenie.SesjaTreningowa.IdUøytkownika != _menedøerUøytkownika.GetUserId(User)) return Unauthorized();
        return View(Êwiczenie);
    }

    [HttpPost, ActionName("UsuÒ")]
    public IActionResult PotwierdüUsuniÍcie(int id)
    {
        var Êwiczenie = _kontekst.Wykonane∆wiczenia.Find(id);
        if (Êwiczenie != null && Êwiczenie.SesjaTreningowa.IdUøytkownika == _menedøerUøytkownika.GetUserId(User))
        {
            _kontekst.Wykonane∆wiczenia.Remove(Êwiczenie);
            _kontekst.SaveChanges();
        }
        return RedirectToAction(nameof(Lista));
    }
}
    