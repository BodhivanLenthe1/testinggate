using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zorgboerderij.Entities;
using System.Linq;
using System.Runtime.Intrinsics.Arm;

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

    public IActionResult Client(int id, string dag)
    {
        var client = _context.clienten.FirstOrDefault(c => c.persid == id);
        if (client == null) return NotFound();

        var dagplanning = _context.Dagindelingen
            .Include(dp => dp.bakje)
            .Where(dp => dp.clientId == id && dp.dagId == dag)
            .OrderBy(dp => dp.volgorde)
            .ToList();

        var clienten = _context.clienten.ToList();

        ViewBag.Client = client;
        ViewBag.Dag = dag;
        ViewBag.Clienten = clienten;

        return View(dagplanning);
    }

    public IActionResult Bewerken(int id, string dag)
    {
        var client = _context.clienten.FirstOrDefault(c => c.persid == id);
        if (client == null) return NotFound();

        var dagplanning = _context.Dagindelingen
            .Where(dp => dp.clientId == id && dp.dagId == dag)
            .OrderBy(dp => dp.volgorde)
            .ToList();

        ViewBag.Client = client;
        ViewBag.Dag = dag;

        return View(dagplanning);
    }

    [HttpPost]
    public IActionResult SavePlanning(int persid, string dag, List<Dagindeling> taken)
    {
        foreach (var taak in taken)
        {
            var bestaande = _context.Dagindelingen.FirstOrDefault(d => d.Id == taak.Id);
            if (bestaande != null)
            {
                bestaande.soort = taak.soort;
                bestaande.volgorde = taak.volgorde;
            }
        }

        _context.SaveChanges();
        return RedirectToAction("Bewerken", new { id = persid, dag = dag });
    }
}
