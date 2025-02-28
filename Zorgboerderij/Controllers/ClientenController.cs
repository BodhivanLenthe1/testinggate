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
    public class ClientenController : Controller
    {
        private readonly AppDbContext _context;

        public ClientenController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Clienten
        public async Task<IActionResult> Index()
        {
            return View(await _context.clienten.ToListAsync());
        }

        // GET: Clienten/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clienten = await _context.clienten
                .FirstOrDefaultAsync(m => m.persid == id);
            if (clienten == null)
            {
                return NotFound();
            }

            return View(clienten);
        }

        // GET: Clienten/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clienten/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("persid,Voornaam,Achternaam,FotoFile,Maandag,Dinsdag,Woensdag,Donderdag,Vrijdag,Zaterdag,Afwezig,Goepskleur")] Clienten clienten)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clienten);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(clienten);
        }

        // GET: Clienten/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clienten = await _context.clienten.FindAsync(id);
            if (clienten == null)
            {
                return NotFound();
            }
            return View(clienten);
        }

        // POST: Clienten/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("persid,Voornaam,Achternaam,FotoFile,Maandag,Dinsdag,Woensdag,Donderdag,Vrijdag,Zaterdag,Afwezig,Goepskleur")] Clienten clienten)
        {
            if (id != clienten.persid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clienten);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientenExists(clienten.persid))
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
            return View(clienten);
        }

        // GET: Clienten/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clienten = await _context.clienten
                .FirstOrDefaultAsync(m => m.persid == id);
            if (clienten == null)
            {
                return NotFound();
            }

            return View(clienten);
        }

        // POST: Clienten/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clienten = await _context.clienten.FindAsync(id);
            if (clienten != null)
            {
                _context.clienten.Remove(clienten);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientenExists(int id)
        {
            return _context.clienten.Any(e => e.persid == id);
        }
    }
}
