using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Zorgboerderij.Entities;

namespace Zorgboerderij.Controllers
{
    public class AfmeldingenController : Controller
    {
        private readonly AppDbContext _context;

        public AfmeldingenController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize]
        // GET: Afmeldingen
        public async Task<IActionResult> Index()
        {
            return View(await _context.afmeldingen.ToListAsync());
        }

        [Authorize]
        // GET: Afmeldingen/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var afmeldingen = await _context.afmeldingen
                .FirstOrDefaultAsync(m => m.persid == id);
            if (afmeldingen == null)
            {
                return NotFound();
            }

            return View(afmeldingen);
        }

        [Authorize]
        // GET: Afmeldingen/Create
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        // POST: Afmeldingen/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("persid,Tandarts,Vakantie,Verjaardag,Ziek,Vrij,Dokter,OpKarwei")] Afmeldingen afmeldingen)
        {
            if (ModelState.IsValid)
            {
                _context.Add(afmeldingen);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(afmeldingen);
        }

        [Authorize]
        // GET: Afmeldingen/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var afmeldingen = await _context.afmeldingen.FindAsync(id);
            if (afmeldingen == null)
            {
                return NotFound();
            }
            return View(afmeldingen);
        }

        [Authorize]
        // POST: Afmeldingen/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("persid,Tandarts,Vakantie,Verjaardag,Ziek,Vrij,Dokter,OpKarwei")] Afmeldingen afmeldingen)
        {
            if (id != afmeldingen.persid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(afmeldingen);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AfmeldingenExists(afmeldingen.persid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(afmeldingen);
        }

        [Authorize]
        // GET: Afmeldingen/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var afmeldingen = await _context.afmeldingen
                .FirstOrDefaultAsync(m => m.persid == id);
            if (afmeldingen == null)
            {
                return NotFound();
            }

            return View(afmeldingen);
        }

        [Authorize]
        // POST: Afmeldingen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var afmeldingen = await _context.afmeldingen.FindAsync(id);
            if (afmeldingen != null)
            {
                _context.afmeldingen.Remove(afmeldingen);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AfmeldingenExists(int id)
        {
            return _context.afmeldingen.Any(e => e.persid == id);
        }
    }
}
