using Microsoft.AspNetCore.Mvc;
using Zorgboerderij.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

public class DagindelingController : Controller
{
    private readonly AppDbContext _context;

    public DagindelingController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var dagen = new[] { "Maandag", "Dinsdag", "Woensdag", "Donderdag", "Vrijdag", "Zaterdag" };

        var overzicht = dagen.Select(dag => new DagBezettingViewModel
        {
            Dag = dag,
            AantalClienten = _context.clienten.Count(c => EF.Property<string>(c, dag) != "X"),
            AantalPersoneel = _context.personeel.Count(p => EF.Property<string>(p, dag) != "X")
        }).ToList();

        return View("Dagindeling", overzicht);
    }

    private string GetNederlandseDagNaam(DayOfWeek dag)
    {
        return dag switch
        {
            DayOfWeek.Monday => "Maandag",
            DayOfWeek.Tuesday => "Dinsdag",
            DayOfWeek.Wednesday => "Woensdag",
            DayOfWeek.Thursday => "Donderdag",
            DayOfWeek.Friday => "Vrijdag",
            DayOfWeek.Saturday => "Zaterdag",
            DayOfWeek.Sunday => "Zondag",
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public IActionResult Dag(string dag)
    {
        if (string.IsNullOrEmpty(dag)) return BadRequest("Geen dag geselecteerd.");
        ViewBag.DebugDag = dag;

        var personeel = _context.personeel.Where(c => EF.Property<string>(c, dag) != "X").ToList();
        var clienten = _context.clienten.Where(c => EF.Property<string>(c, dag) != "X").ToList();

        ViewBag.Personeel = personeel;
        return View("Dag", clienten);
    }

    public IActionResult Client(int id, string dag)
    {
        var client = _context.clienten.FirstOrDefault(c => c.persid == id);
        if (client == null) return NotFound();
        if (string.IsNullOrEmpty(dag)) dag = GetNederlandseDagNaam(DateTime.Today.DayOfWeek);

        ViewBag.DebugDag = dag;

        var planning = _context.Dagindelingen
            .Include(d => d.bakje)
            .Include(d => d.client)
            .Where(d => d.clientId == id && d.dagId == dag)
            .OrderBy(d => d.volgorde)
            .ToList();

        var bakjes = _context.bakjes.ToList();
        var alleClienten = _context.clienten.Where(c => EF.Property<string>(c, dag) != "X").ToList();

        var viewModel = new DagindelingViewModel
        {
            Client = client,
            Dagindelingen = planning,
            Bakjes = bakjes,
            AlleClienten = alleClienten
        };

        return View("Client", viewModel);
    }

    public IActionResult ClientPlanning(int clientId, string dag)
    {
        var client = _context.clienten.FirstOrDefault(c => c.persid == clientId);
        if (client == null) return NotFound();
        if (string.IsNullOrEmpty(dag)) dag = GetNederlandseDagNaam(DateTime.Today.DayOfWeek);

        ViewBag.DebugDag = dag;

        var dagindeling = _context.Dagindelingen.Where(d => d.clientId == clientId).ToList();
        var bakjes = _context.bakjes.ToList();
        var personeel = _context.personeel.ToList();
        var alleClienten = _context.clienten.Where(c => EF.Property<string>(c, dag) != "X").ToList();

        var viewModel = new DagindelingViewModel
        {
            Client = client,
            Dagindelingen = dagindeling,
            Bakjes = bakjes,
            Personeel = personeel,
            Clienten = new System.Collections.Generic.List<Clienten> { client },
            AlleClienten = alleClienten
        };

        return View(viewModel);
    }

    [HttpPost]
    public IActionResult UpdateVolgorde(int clientId, string nieuweVolgorde, string dag)
    {
        if (string.IsNullOrWhiteSpace(nieuweVolgorde) || string.IsNullOrWhiteSpace(dag))
            return BadRequest("Geen volgorde of dag ontvangen.");

        var datum = DateTime.Today;
        var dagNaam = dag;

        var bestaande = _context.Dagindelingen
            .Where(d => d.clientId == clientId && d.dagId == dagNaam)
            .ToList();
        _context.Dagindelingen.RemoveRange(bestaande);

        var samenwerkingDict = Request.Form.Keys
            .Where(k => k.StartsWith("Samenwerkers["))
            .ToDictionary(
                k => k,
                k => Request.Form[k].ToString()
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => int.TryParse(s, out var sid) ? sid : -1)
                    .Where(id => id > 0)
                    .ToList()
            );

        var bidLijst = nieuweVolgorde.Split(',')
            .Select(s => int.TryParse(s, out var x) ? x : -1)
            .Where(x => x != -1)
            .ToList();

        for (int volg = 0; volg < bidLijst.Count; volg++)
        {
            int bid = bidLijst[volg];
            var bakje = _context.bakjes.FirstOrDefault(b => b.bid == bid);
            if (bakje == null) continue;

            var samenwerkingKey = $"Samenwerkers[{bid}_{volg}]";
            var samenwerkerIds = samenwerkingDict.ContainsKey(samenwerkingKey)
                ? samenwerkingDict[samenwerkingKey]
                : new List<int>();

            var nieuwe = new Dagindeling
            {
                clientId = clientId,
                bid = bid,
                kleur = bakje.Kleur,
                soort = "standaard",
                volgorde = volg,
                datum = datum,
                dagId = dagNaam,
                sid = samenwerkerIds.Count > 0 ? samenwerkerIds[0] : null,
                sid2 = samenwerkerIds.Count > 1 ? samenwerkerIds[1] : null
            };

            _context.Dagindelingen.Add(nieuwe);
        }

        _context.SaveChanges();

        return RedirectToAction("Client", new { id = clientId, dag = dagNaam });
    }

}
