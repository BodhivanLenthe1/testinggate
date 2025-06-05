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

        var weekStart = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday);
        int offset = dag switch
        {
            "Maandag" => 0,
            "Dinsdag" => 1,
            "Woensdag" => 2,
            "Donderdag" => 3,
            "Vrijdag" => 4,
            "Zaterdag" => 5,
            "Zondag" => 6,
            _ => 0
        };
        var dagDatum = weekStart.AddDays(offset);

        ViewBag.Client = client;
        ViewBag.Dag = dag;
        ViewBag.Clienten = clienten;
        ViewBag.DagDatum = dagDatum.ToString("yyyy-MM-dd");

        var afgerondBids = _context.AfgerondeTaken
            .Where(a => a.persid == id && a.dag == dag && a.plandatum == dagDatum)
            .Select(a => a.bid)
            .ToList();
        ViewBag.AfgerondBids = afgerondBids;

        return View(dagplanning);
    }


    public IActionResult Bewerken(int id, string dag)
    {
        var client = _context.clienten.FirstOrDefault(c => c.persid == id);
        if (client == null) return NotFound();

        var dagplanning = _context.Dagindelingen
            .Include(dp => dp.bakje)
            .Where(dp => dp.clientId == id && dp.dagId == dag)
            .OrderBy(dp => dp.volgorde)
            .ToList();

        var bakjes = _context.bakjes.ToList();

        var model = new Zorgboerderij.Models.DagplanningBewerkenViewModel
        {
            Client = client,
            Dag = dag,
            Dagplanning = dagplanning,
            Bakjes = bakjes
        };

        return View(model);
    }

    [HttpPost]
    public IActionResult SavePlanning(int persid, string dag, string nieuweVolgorde)
    {
        var taken = _context.Dagindelingen
            .Where(d => d.clientId == persid && d.dagId == dag)
            .ToList();

        var parsed = (nieuweVolgorde ?? string.Empty)
            .Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select((s, idx) => new { Parts = s.Split(':'), Index = idx })
            .Where(x => x.Parts.Length == 2)
            .Select(x => new { Id = int.Parse(x.Parts[0]), Extra = x.Parts[1] == "1", Order = x.Index })
            .ToList();

        foreach (var item in parsed)
        {
            var bestaande = taken.FirstOrDefault(t => t.bid == item.Id);
            if (bestaande != null)
            {
                bestaande.volgorde = item.Order;
            }
            else if (item.Extra)
            {
                var bakje = _context.bakjes.FirstOrDefault(b => b.bid == item.Id);
                if (bakje != null)
                {
                    _context.Dagindelingen.Add(new Dagindeling
                    {
                        clientId = persid,
                        dagId = dag,
                        bid = item.Id,
                        kleur = bakje.Kleur,
                        soort = "tijdelijk",
                        volgorde = item.Order,
                        datum = DateTime.Now.Date
                    });
                }
            }
        }

        _context.SaveChanges();
        return RedirectToAction("Bewerken", new { id = persid, dag = dag });
    }

    [HttpPost]
    public IActionResult TaakAfvinken(int bid, int persid, string dag, string plandatum, int? sid, int? sid2)
    {
        DateTime planDt = DateTime.Parse(plandatum);
        var bestaat = _context.AfgerondeTaken.FirstOrDefault(a =>
            a.bid == bid &&
            a.persid == persid &&
            a.dag == dag &&
            a.plandatum == planDt
        );
        if (bestaat == null)
        {
            var taak = new AfgerondeTaak
            {
                bid = bid,
                persid = persid,
                sid = sid,
                sid2 = sid2,
                dag = dag,
                plandatum = planDt,
                datum_afronden = DateTime.Now.Date,
                tijd_afronden = DateTime.Now.TimeOfDay
            };
            _context.AfgerondeTaken.Add(taak);
            _context.SaveChanges();
        }
        return Json(new { success = true });
    }

    [HttpPost]
    public IActionResult TaakHeropenen(int bid, int persid, string dag, string plandatum)
    {
        DateTime planDt = DateTime.Parse(plandatum);
        var bestaat = _context.AfgerondeTaken.FirstOrDefault(a =>
            a.bid == bid &&
            a.persid == persid &&
            a.dag == dag &&
            a.plandatum == planDt
        );
        if (bestaat != null)
        {
            _context.AfgerondeTaken.Remove(bestaat);
            _context.SaveChanges();
        }
        return Json(new { success = true });
    }
}
