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
    public class PersoneelController : Controller
    {
        private readonly AppDbContext _context;

        public PersoneelController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Personeel
        public async Task<IActionResult> Index()
        {
            return View(await _context.personeel.ToListAsync());
        }

        // GET: Personeel/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personeel = await _context.personeel
                .FirstOrDefaultAsync(m => m.persid == id);
            if (personeel == null)
            {
                return NotFound();
            }

            return View(personeel);
        }

        // GET: Personeel/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Personeel/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("persid,Voornaam,Achternaam,FotoFile,Maandag,Dinsdag,Woensdag,Donderdag,Vrijdag,Zaterdag,Groepskleur")] Personeel personeel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(personeel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(personeel);
        }

        // GET: Personeel/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personeel = await _context.personeel.FindAsync(id);
            if (personeel == null)
            {
                return NotFound();
            }
            return View(personeel);
        }

        // POST: Personeel/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("persid,Voornaam,Achternaam,FotoFile,Maandag,Dinsdag,Woensdag,Donderdag,Vrijdag,Zaterdag,Groepskleur")] Personeel personeel)
        {
            if (id != personeel.persid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(personeel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersoneelExists(personeel.persid))
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
            return View(personeel);
        }

        // GET: Personeel/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personeel = await _context.personeel
                .FirstOrDefaultAsync(m => m.persid == id);
            if (personeel == null)
            {
                return NotFound();
            }

            return View(personeel);
        }

        // POST: Personeel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var personeel = await _context.personeel.FindAsync(id);
            if (personeel != null)
            {
                _context.personeel.Remove(personeel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersoneelExists(int id)
        {
            return _context.personeel.Any(e => e.persid == id);
        }
    }
}
