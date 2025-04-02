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

        
        public async Task<IActionResult> Index()
        {
            return View(await _context.clienten.ToListAsync());
        }


      
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

        public IActionResult Create()
        {
            return View();
        }

        
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
            {
                return NotFound();
            }

            return View(clienten);
        }

        
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
