using Microsoft.AspNetCore.Mvc;
using Zorgboerderij.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Zorgboerderij.Controllers
{
    public class BezettingenController : Controller
    {
        private readonly AppDbContext _context;
        public BezettingenController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var days = new List<string> { "Maandag", "Dinsdag", "Woensdag", "Donderdag", "Vrijdag", "Zaterdag" };
            return View(days);
        }

        public IActionResult Dagbezetting(string dag)
        {
            if (string.IsNullOrEmpty(dag))
            {
                return RedirectToAction("Index");
            }

            var clientRecords = _context.clienten.Where(c =>
                (dag == "Maandag" && c.Maandag != null && c.Maandag.ToLower() != "x") ||
                (dag == "Dinsdag" && c.Dinsdag != null && c.Dinsdag.ToLower() != "x") ||
                (dag == "Woensdag" && c.Woensdag != null && c.Woensdag.ToLower() != "x") ||
                (dag == "Donderdag" && c.Donderdag != null && c.Donderdag.ToLower() != "x") ||
                (dag == "Vrijdag" && c.Vrijdag != null && c.Vrijdag.ToLower() != "x") ||
                (dag == "Zaterdag" && c.Zaterdag != null && c.Zaterdag.ToLower() != "x")
            ).ToList();

            var personeelRecords = _context.personeel.Where(p =>
                (dag == "Maandag" && p.Maandag != null && p.Maandag.ToLower() != "x") ||
                (dag == "Dinsdag" && p.Dinsdag != null && p.Dinsdag.ToLower() != "x") ||
                (dag == "Woensdag" && p.Woensdag != null && p.Woensdag.ToLower() != "x") ||
                (dag == "Donderdag" && p.Donderdag != null && p.Donderdag.ToLower() != "x") ||
                (dag == "Vrijdag" && p.Vrijdag != null && p.Vrijdag.ToLower() != "x") ||
                (dag == "Zaterdag" && p.Zaterdag != null && p.Zaterdag.ToLower() != "x")
            ).ToList();

            var model = new DagBezettingViewModel
            {
                Dag = dag,
                AantalClienten = clientRecords.Count,
                AantalPersoneel = personeelRecords.Count,
                ClientList = clientRecords,
                PersoneelList = personeelRecords
            };

            switch (dag)
            {
                case "Maandag":
                    model.ClientOchtend = _context.clienten.Count(c => c.Maandag != null && c.Maandag.ToLower() == "o");
                    model.ClientMiddag = _context.clienten.Count(c => c.Maandag != null && c.Maandag.ToLower() == "m");
                    model.ClientHeleDag = _context.clienten.Count(c => c.Maandag != null && c.Maandag.ToLower() == "h");
                    model.PersoneelOchtend = _context.personeel.Count(p => p.Maandag != null && p.Maandag.ToLower() == "o");
                    model.PersoneelMiddag = _context.personeel.Count(p => p.Maandag != null && p.Maandag.ToLower() == "m");
                    model.PersoneelHeleDag = _context.personeel.Count(p => p.Maandag != null && p.Maandag.ToLower() == "h");
                    break;
                case "Dinsdag":
                    model.ClientOchtend = _context.clienten.Count(c => c.Dinsdag != null && c.Dinsdag.ToLower() == "o");
                    model.ClientMiddag = _context.clienten.Count(c => c.Dinsdag != null && c.Dinsdag.ToLower() == "m");
                    model.ClientHeleDag = _context.clienten.Count(c => c.Dinsdag != null && c.Dinsdag.ToLower() == "h");
                    model.PersoneelOchtend = _context.personeel.Count(p => p.Dinsdag != null && p.Dinsdag.ToLower() == "o");
                    model.PersoneelMiddag = _context.personeel.Count(p => p.Dinsdag != null && p.Dinsdag.ToLower() == "m");
                    model.PersoneelHeleDag = _context.personeel.Count(p => p.Dinsdag != null && p.Dinsdag.ToLower() == "h");
                    break;
                case "Woensdag":
                    model.ClientOchtend = _context.clienten.Count(c => c.Woensdag != null && c.Woensdag.ToLower() == "o");
                    model.ClientMiddag = _context.clienten.Count(c => c.Woensdag != null && c.Woensdag.ToLower() == "m");
                    model.ClientHeleDag = _context.clienten.Count(c => c.Woensdag != null && c.Woensdag.ToLower() == "h");
                    model.PersoneelOchtend = _context.personeel.Count(p => p.Woensdag != null && p.Woensdag.ToLower() == "o");
                    model.PersoneelMiddag = _context.personeel.Count(p => p.Woensdag != null && p.Woensdag.ToLower() == "m");
                    model.PersoneelHeleDag = _context.personeel.Count(p => p.Woensdag != null && p.Woensdag.ToLower() == "h");
                    break;
                case "Donderdag":
                    model.ClientOchtend = _context.clienten.Count(c => c.Donderdag != null && c.Donderdag.ToLower() == "o");
                    model.ClientMiddag = _context.clienten.Count(c => c.Donderdag != null && c.Donderdag.ToLower() == "m");
                    model.ClientHeleDag = _context.clienten.Count(c => c.Donderdag != null && c.Donderdag.ToLower() == "h");
                    model.PersoneelOchtend = _context.personeel.Count(p => p.Donderdag != null && p.Donderdag.ToLower() == "o");
                    model.PersoneelMiddag = _context.personeel.Count(p => p.Donderdag != null && p.Donderdag.ToLower() == "m");
                    model.PersoneelHeleDag = _context.personeel.Count(p => p.Donderdag != null && p.Donderdag.ToLower() == "h");
                    break;
                case "Vrijdag":
                    model.ClientOchtend = _context.clienten.Count(c => c.Vrijdag != null && c.Vrijdag.ToLower() == "o");
                    model.ClientMiddag = _context.clienten.Count(c => c.Vrijdag != null && c.Vrijdag.ToLower() == "m");
                    model.ClientHeleDag = _context.clienten.Count(c => c.Vrijdag != null && c.Vrijdag.ToLower() == "h");
                    model.PersoneelOchtend = _context.personeel.Count(p => p.Vrijdag != null && p.Vrijdag.ToLower() == "o");
                    model.PersoneelMiddag = _context.personeel.Count(p => p.Vrijdag != null && p.Vrijdag.ToLower() == "m");
                    model.PersoneelHeleDag = _context.personeel.Count(p => p.Vrijdag != null && p.Vrijdag.ToLower() == "h");
                    break;
                case "Zaterdag":
                    model.ClientOchtend = _context.clienten.Count(c => c.Zaterdag != null && c.Zaterdag.ToLower() == "o");
                    model.ClientMiddag = _context.clienten.Count(c => c.Zaterdag != null && c.Zaterdag.ToLower() == "m");
                    model.ClientHeleDag = _context.clienten.Count(c => c.Zaterdag != null && c.Zaterdag.ToLower() == "h");
                    model.PersoneelOchtend = _context.personeel.Count(p => p.Zaterdag != null && p.Zaterdag.ToLower() == "o");
                    model.PersoneelMiddag = _context.personeel.Count(p => p.Zaterdag != null && p.Zaterdag.ToLower() == "m");
                    model.PersoneelHeleDag = _context.personeel.Count(p => p.Zaterdag != null && p.Zaterdag.ToLower() == "h");
                    break;
                default:
                    break;
            }
            return View(model);
        }
    }
}
