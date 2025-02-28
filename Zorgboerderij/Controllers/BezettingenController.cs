using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Zorgboerderij.Entities;

namespace Zorgboerderij.Controllers
{
    public class BezettingenController : Controller
    {
        private readonly AppDbContext _context;

        public BezettingenController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize]
        // GET: Bezettingen
        public async Task<IActionResult> Index()
        {
            return View(await _context.Bezettingen.ToListAsync());
        }

        [Authorize]
        // GET: Bezettingen/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bezettingen = await _context.Bezettingen
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bezettingen == null)
            {
                return NotFound();
            }

            return View(bezettingen);
        }

        [Authorize]
        // GET: Bezettingen/Create
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        // POST: Bezettingen/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Client,Maandag,Dinsdag,Woensdag,Donderdag,Vrijdag,Zaterdag,Totaal")] Bezettingen bezettingen)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bezettingen);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bezettingen);
        }

        [Authorize]
        // GET: Bezettingen/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bezettingen = await _context.Bezettingen.FindAsync(id);
            if (bezettingen == null)
            {
                return NotFound();
            }
            return View(bezettingen);
        }

        [Authorize]
        // POST: Bezettingen/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Client,Maandag,Dinsdag,Woensdag,Donderdag,Vrijdag,Zaterdag,Totaal")] Bezettingen bezettingen)
        {
            if (id != bezettingen.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bezettingen);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BezettingenExists(bezettingen.Id))
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
            return View(bezettingen);
        }

        [Authorize]
        // GET: Bezettingen/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bezettingen = await _context.Bezettingen
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bezettingen == null)
            {
                return NotFound();
            }

            return View(bezettingen);
        }

        [Authorize]
        // POST: Bezettingen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bezettingen = await _context.Bezettingen.FindAsync(id);
            if (bezettingen != null)
            {
                _context.Bezettingen.Remove(bezettingen);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BezettingenExists(int id)
        {
            return _context.Bezettingen.Any(e => e.Id == id);
        }
    }
}
