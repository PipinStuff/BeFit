using BeFit.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BeFit.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

[Authorize]
public class Wykonane�wiczeniaController : Controller
{
    private readonly ApplicationDbContext _kontekst;
    private readonly UserManager<U�ytkownik> _mened�erU�ytkownika;

    public Wykonane�wiczeniaController(ApplicationDbContext kontekst, UserManager<U�ytkownik> mened�erU�ytkownika)
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
    public IActionResult Dodaj(Wykonane�wiczenie model)
    {
        if (ModelState.IsValid)
        {
            var idU�ytkownika = _mened�erU�ytkownika.GetUserId(User);
            var sesja = _kontekst.SesjeTreningowe.Find(model.IdSesjiTreningowej);

            if (sesja == null || sesja.IdU�ytkownika != idU�ytkownika) return Unauthorized();

            _kontekst.Wykonane�wiczenia.Add(model);
            _kontekst.SaveChanges();
            return RedirectToAction(nameof(Lista));
        }
        return View(model);
    }

    public IActionResult Edytuj(int id)
    {
        var �wiczenie = _kontekst.Wykonane�wiczenia.Find(id);
        if (�wiczenie == null || �wiczenie.SesjaTreningowa.IdU�ytkownika != _mened�erU�ytkownika.GetUserId(User)) return Unauthorized();
        return View(�wiczenie);
    }

    [HttpPost]
    public IActionResult Edytuj(Wykonane�wiczenie model)
    {
        if (ModelState.IsValid)
        {
            _kontekst.Wykonane�wiczenia.Update(model);
            _kontekst.SaveChanges();
            return RedirectToAction(nameof(Lista));
        }
        return View(model);
    }

    public IActionResult Usu�(int id)
    {
        var �wiczenie = _kontekst.Wykonane�wiczenia.Find(id);
        if (�wiczenie == null || �wiczenie.SesjaTreningowa.IdU�ytkownika != _mened�erU�ytkownika.GetUserId(User)) return Unauthorized();
        return View(�wiczenie);
    }

    [HttpPost, ActionName("Usu�")]
    public IActionResult Potwierd�Usuni�cie(int id)
    {
        var �wiczenie = _kontekst.Wykonane�wiczenia.Find(id);
        if (�wiczenie != null && �wiczenie.SesjaTreningowa.IdU�ytkownika == _mened�erU�ytkownika.GetUserId(User))
        {
            _kontekst.Wykonane�wiczenia.Remove(�wiczenie);
            _kontekst.SaveChanges();
        }
        return RedirectToAction(nameof(Lista));
    }
}
    