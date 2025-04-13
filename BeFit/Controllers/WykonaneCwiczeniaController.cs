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
    private readonly UserManager<U�ytkownik> _mened�erU�ytkownika;

    public WykonaneCwiczeniaController(ApplicationDbContext kontekst, UserManager<U�ytkownik> mened�erU�ytkownika)
    {
        _kontekst = kontekst;
        _mened�erU�ytkownika = mened�erU�ytkownika;
    }

    public IActionResult Lista(int id)
    {
        var �wiczenia = _kontekst.Wykonane�wiczenia
            .Where(c => c.IdSesjiTreningowej == id)
            .Include(c => c.Typ�wiczenia)
            .ToList();

        ViewBag.SesjaId = id;
        return View(�wiczenia);
    }


    public IActionResult Dodaj()
    {
        var idU�ytkownika = _mened�erU�ytkownika.GetUserId(User);
        ViewBag.Typy�wicze� = new SelectList(_kontekst.Typy�wicze�, "Id", "Nazwa");
        ViewBag.SesjeTreningowe = new SelectList(_kontekst.SesjeTreningowe.Where(s => s.IdU�ytkownika == idU�ytkownika), "Id", "DataRozpocz�cia");
        return View();
    }


    [HttpPost]
    public IActionResult Dodaj(int id,Wykonane�wiczenie model)
    {
        var idU�ytkownika = _mened�erU�ytkownika.GetUserId(User);
        model.IdSesjiTreningowej = id;
        var sesja = _kontekst.SesjeTreningowe
            .FirstOrDefault(s => s.Id == model.IdSesjiTreningowej);

        if (sesja == null || sesja.IdU�ytkownika != idU�ytkownika)
        {
            return Unauthorized();
        }

        var typCwiczenia = _kontekst.Typy�wicze�.FirstOrDefault(t => t.Id == model.IdTypu�wiczenia);
        if (typCwiczenia == null)
        {
            ModelState.AddModelError(nameof(model.IdTypu�wiczenia), "Wybrany typ �wiczenia nie istnieje.");
            ViewBag.Typy�wicze� = new SelectList(_kontekst.Typy�wicze�, "Id", "Nazwa");
            return View(model);
        }

        model.SesjaTreningowa = sesja;
        model.Typ�wiczenia = typCwiczenia;
        model=new Wykonane�wiczenie
        {
            IdSesjiTreningowej = model.IdSesjiTreningowej,
            IdTypu�wiczenia = model.IdTypu�wiczenia,
            Obci��enie = model.Obci��enie,
            LiczbaSerii = model.LiczbaSerii,
            Powt�rzenia = model.Powt�rzenia,
            SesjaTreningowa=model.SesjaTreningowa,
            Typ�wiczenia = model.Typ�wiczenia
        };
        _kontekst.Wykonane�wiczenia.Add(model);
        _kontekst.SaveChanges();

        return RedirectToAction(nameof(Lista), new { id = model.IdSesjiTreningowej });
    }

    public IActionResult Edytuj(int id)
    {
        var �wiczenie = _kontekst.Wykonane�wiczenia
    .Include(c => c.SesjaTreningowa)
    .Include(c => c.Typ�wiczenia)
    .FirstOrDefault(c => c.Id == id);
        if (�wiczenie == null || �wiczenie.SesjaTreningowa.IdU�ytkownika != _mened�erU�ytkownika.GetUserId(User)) return Unauthorized();
        ViewBag.Typy�wicze� = new SelectList(_kontekst.Typy�wicze�, "Id", "Nazwa", �wiczenie.IdTypu�wiczenia);
        return View(�wiczenie);
    }

    [HttpPost]
    public IActionResult Edytuj(Wykonane�wiczenie model)
    {
        model.Typ�wiczenia = _kontekst.Typy�wicze�.FirstOrDefault(t => t.Id == model.IdTypu�wiczenia);
        model.SesjaTreningowa=  _kontekst.SesjeTreningowe.FirstOrDefault(s => s.Id == model.IdSesjiTreningowej);
        _kontekst.Wykonane�wiczenia.Update(model);
            _kontekst.SaveChanges();
        return RedirectToAction(nameof(Lista), new { id = model.IdSesjiTreningowej });

    }

    public IActionResult Usu�(int id)
    {
        var �wiczenie = _kontekst.Wykonane�wiczenia
    .Include(c => c.SesjaTreningowa)
    .Include(c => c.Typ�wiczenia)
    .FirstOrDefault(c => c.Id == id);
        if (�wiczenie == null || �wiczenie.SesjaTreningowa.IdU�ytkownika != _mened�erU�ytkownika.GetUserId(User)) return Unauthorized();
        return View(�wiczenie);
    }

    [HttpPost, ActionName("PotwierdzUsuniecie")]
    public IActionResult PotwierdzUsuniecie(int id)
    {
        var �wiczenie = _kontekst.Wykonane�wiczenia
    .Include(c => c.SesjaTreningowa)
    .Include(c => c.Typ�wiczenia)
    .FirstOrDefault(c => c.Id == id);
        if (�wiczenie != null && �wiczenie.SesjaTreningowa.IdU�ytkownika == _mened�erU�ytkownika.GetUserId(User))
        {
            _kontekst.Wykonane�wiczenia.Remove(�wiczenie);
            _kontekst.SaveChanges();
        }
        return RedirectToAction(nameof(Lista), new { id = �wiczenie.IdSesjiTreningowej });
    }
}
