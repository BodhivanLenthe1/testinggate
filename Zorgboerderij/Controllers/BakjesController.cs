using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Zorgboerderij.Entities;

namespace Zorgboerderij.Controllers
{
    public class BakjesController : Controller
    {
        private readonly AppDbContext _context;

        public BakjesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Bakjes
        public async Task<IActionResult> Index()
        {
            return View(await _context.bakjes.ToListAsync());
        }

        // GET: Bakjes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bakjes = await _context.bakjes
                .FirstOrDefaultAsync(m => m.bid == id);
            if (bakjes == null)
            {
                return NotFound();
            }

            return View(bakjes);
        }

        // GET: Bakjes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bakjes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("bid,Foto,Titel,Kleur")] Bakjes bakjes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bakjes);
                await _context.SaveChangesAsync();
                return Content("<script type='text/javascript'>window.history.go(-2);</script>", "text/html");
            }
            return View(bakjes);
        }

        // GET: Bakjes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bakjes = await _context.bakjes
                .FirstOrDefaultAsync(m => m.bid == id);
            if (bakjes == null)
            {
                return NotFound();
            }

            return View(bakjes);
        }

        // POST: Bakjes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bakjes = await _context.bakjes.FindAsync(id);
            if (bakjes != null)
            {
                _context.bakjes.Remove(bakjes);
            }

            await _context.SaveChangesAsync();
            return Content("<script type='text/javascript'>window.history.go(-2);</script>", "text/html");
        }

        private bool BakjesExists(int id)
        {
            return _context.bakjes.Any(e => e.bid == id);
        }

        public IActionResult DierenIndex()
        {
            var filteredBakjes = _context.bakjes.Where(b => b.Kleur == "Blauw").ToList();

            return View(filteredBakjes);
        }

        public IActionResult VerzorgenIndex()
        {
            var filteredBakjes = _context.bakjes.Where(b => b.Kleur == "Paars").ToList();

            return View(filteredBakjes);
        }

        public IActionResult TuinIndex()
        {
            var filteredBakjes = _context.bakjes.Where(b => b.Kleur == "Groen").ToList();

            return View(filteredBakjes);
        }

        public IActionResult LandIndex()
        {
            var filteredBakjes = _context.bakjes.Where(b => b.Kleur == "Oranje").ToList();

            return View(filteredBakjes);
        }

        public IActionResult TijdenIndex()
        {
            var filteredBakjes = _context.bakjes.Where(b => b.Kleur == "Bruin").ToList();

            return View(filteredBakjes);
        }

        public IActionResult CorveeIndex()
        {
            var filteredBakjes = _context.bakjes.Where(b => b.Kleur == "Geel").ToList();

            return View(filteredBakjes);
        }

        public IActionResult CreatiefIndex()
        {
            var filteredBakjes = _context.bakjes.Where(b => b.Kleur == "Rood").ToList();

            return View(filteredBakjes);
        }

        public IActionResult SchuurIndex()
        {
            var filteredBakjes = _context.bakjes.Where(b => b.Kleur == "Grijs").ToList();

            return View(filteredBakjes);
        }

        public IActionResult KlussenIndex()
        {
            var filteredBakjes = _context.bakjes.Where(b => b.Kleur == "Zwart").ToList();

            return View(filteredBakjes);
        }
    }
}
