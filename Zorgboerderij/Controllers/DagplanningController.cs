using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zorgboerderij.Entities;

namespace Zorgboerderij.Controllers
{
    public class DagplanningController : Controller
    {
        private readonly AppDbContext _context;
        public DagplanningController(AppDbContext context)
            => _context = context;

        public IActionResult Dagkeuze(string dag, string melding)
        {
            HttpContext.Session.SetString("Dagcode", dag);
            ViewBag.Melding = melding;

            var clienten = _context.clienten
                .Where(c => EF.Property<string>(c, dag) != "X")
                .ToList();
            var personeel = _context.personeel
                .Where(p => EF.Property<string>(p, dag) != "X")
                .ToList();

            ViewBag.Personeel = personeel;
            ViewBag.Dag = dag;
            return View(clienten);
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

            if (string.IsNullOrEmpty(dag))
                dag = GetNederlandseDagNaam(DateTime.Today.DayOfWeek);
            ViewBag.Dag = dag;
            ViewBag.Client = client;

            var weekStart = DateTime.Today
                .AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday);
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
            var planDt = weekStart.AddDays(offset);
            ViewBag.DagDatum = planDt.ToString("yyyy-MM-dd");

            var dagplanning = _context.Dagindelingen
                .Include(dp => dp.bakje)
                .Where(dp =>
                    dp.clientId == id &&
                    dp.dagId == dag &&
                    (
                        dp.soort == "standaard" ||
                        (dp.soort == "eenmalig" && dp.datum == planDt)
                    )
                )
                .OrderBy(dp => dp.volgorde)
                .ToList();

            ViewBag.AfgerondBids = _context.AfgerondeTaken
                .Where(a =>
                    a.persid == id &&
                    a.dag == dag &&
                    a.plandatum == planDt
                )
                .Select(a => a.bid)
                .ToList();

            ViewBag.Clienten = _context.clienten.ToList();

            return View(dagplanning);
        }


        public IActionResult Bewerken(int id, string dag)
        {
            var client = _context.clienten.FirstOrDefault(c => c.persid == id);
            if (client == null) return NotFound();

            if (string.IsNullOrEmpty(dag))
                dag = GetNederlandseDagNaam(DateTime.Today.DayOfWeek);

            ViewBag.Dag = dag;
            ViewBag.Client = client;

            var weekStart = DateTime.Today
                .AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday);
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
            var planDt = weekStart.AddDays(offset);
            ViewBag.DagDatum = planDt.ToString("yyyy-MM-dd");

            var dagplanning = _context.Dagindelingen
                .Include(d => d.bakje)
                .Where(d =>
                    d.clientId == id &&
                    d.dagId == dag &&
                    (
                        d.soort == "standaard"
                        ||
                        (d.soort == "eenmalig" && d.datum == planDt)
                    )
                )
                .OrderBy(d => d.volgorde)
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
            return RedirectToAction("Bewerken", new { id = clientId, dag });
        }


        [HttpPost]
        public IActionResult TaakAfvinken(
            int bid, int persid, string dag,
            string plandatum, int? sid, int? sid2)
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

        [HttpGet]
        public IActionResult GetStandaardDagindeling(int persid, string dag)
        {
            var taken = _context.Dagindelingen
                .Include(x => x.bakje)
                .Where(x =>
                    x.clientId == persid &&
                    x.dagId == dag &&
                    x.soort == "standaard")
                .OrderBy(x => x.volgorde)
                .Select(x => new
                {
                    id = x.bid,
                    titel = x.bakje.Titel,
                    kleur = x.bakje.Kleur.ToLower(),
                    foto = x.bakje.Foto
                })
                .ToList();
            return Json(taken);
        }
    }
}
