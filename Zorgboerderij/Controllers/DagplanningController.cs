using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zorgboerderij.Entities;
using System.Linq;

public class DagplanningController : Controller
{
    private readonly AppDbContext _context;

    public DagplanningController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Dagkeuze(string dag, string melding)
    {
        HttpContext.Session.SetString("Dagcode", dag);
        ViewBag.Melding = melding;

        var clienten = _context.clienten.Where(c => EF.Property<string>(c, dag) != "X").ToList();
        var personeel = _context.personeel.Where(p => EF.Property<string>(p, dag) != "X").ToList();

        ViewBag.Personeel = personeel;
        ViewBag.Dag = dag;

        return View(clienten);
    }


    [HttpPost]
    public IActionResult DagcodeLogin(string dagcode, string wachtwoord)
    {
        var match = _context.dagCodes
            .FirstOrDefault(d => d.Dag == dagcode && d.DagCode == wachtwoord);

        if (match != null)
        {
            HttpContext.Session.Remove("Dagcode");
            return RedirectToAction("Index", "Home");
        }

        TempData["LoginFout"] = "Ongeldige dagcode of wachtwoord.";
        return Redirect(Request.Headers["Referer"].ToString());
    }

    [HttpPost]
    public IActionResult CheckDagcode(string gebruikersnaam, string wachtwoord)
    {
        var sessionDag = HttpContext.Session.GetString("Dagcode");
        if (string.IsNullOrEmpty(sessionDag))
            return Json(new { success = false });

        var dagcode = _context.dagCodes
            .FirstOrDefault(d => d.Dag == sessionDag && d.DagCode == wachtwoord);

        if (dagcode != null)
        {
            HttpContext.Session.Remove("Dagcode");
            return Json(new { success = true });
        }
        return Json(new { success = false });
    }
}
