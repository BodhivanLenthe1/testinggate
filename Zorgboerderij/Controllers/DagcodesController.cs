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
    public class DagcodesController : Controller
    {
        private readonly AppDbContext _context;

        public DagcodesController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize]
        // GET: Dagcodes
        public async Task<IActionResult> Index()
        {
            return View(await _context.dagCodes.ToListAsync());
        }

        [Authorize]
        // GET: Dagcodes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dagcodes = await _context.dagCodes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dagcodes == null)
            {
                return NotFound();
            }

            return View(dagcodes);
        }

        [Authorize]
        // GET: Dagcodes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Dagcodes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Dag,DagCode")] Dagcodes dagcodes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dagcodes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dagcodes);
        }

        [Authorize]
        // GET: Dagcodes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dagcodes = await _context.dagCodes.FindAsync(id);
            if (dagcodes == null)
            {
                return NotFound();
            }
            return View(dagcodes);
        }

        [Authorize]
        // POST: Dagcodes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Dag,DagCode")] Dagcodes dagcodes)
        {
            if (id != dagcodes.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dagcodes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DagcodesExists(dagcodes.Id))
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
            return View(dagcodes);
        }

        [Authorize]
        // GET: Dagcodes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dagcodes = await _context.dagCodes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dagcodes == null)
            {
                return NotFound();
            }

            return View(dagcodes);
        }

        [Authorize]
        // POST: Dagcodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dagcodes = await _context.dagCodes.FindAsync(id);
            if (dagcodes != null)
            {
                _context.dagCodes.Remove(dagcodes);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DagcodesExists(int id)
        {
            return _context.dagCodes.Any(e => e.Id == id);
        }
    }
}
