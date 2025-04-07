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
        public async Task<IActionResult> Create([Bind("Voornaam,Achternaam,FotoFile,Groepskleur")] Clienten clienten)
        {
            clienten.Maandag = Request.Form["Maandag"].ToString();
            clienten.Dinsdag = Request.Form["Dinsdag"].ToString();
            clienten.Woensdag = Request.Form["Woensdag"].ToString();
            clienten.Donderdag = Request.Form["Donderdag"].ToString();
            clienten.Vrijdag = Request.Form["Vrijdag"].ToString();
            clienten.Zaterdag = Request.Form["Zaterdag"].ToString();

            _context.Add(clienten);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
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
        public async Task<IActionResult> Edit(int id, [Bind("persid,Voornaam,Achternaam,FotoFile,Maandag,Dinsdag,Woensdag,Donderdag,Vrijdag,Zaterdag,Groepskleur")] Clienten clienten)
        {
            if (id != clienten.persid)
            {
                return NotFound();
            }

            try
            {
                var existingClient = await _context.clienten.FindAsync(id);
                if (existingClient == null)
                {
                    return NotFound();
                }
                existingClient.Voornaam = clienten.Voornaam;
                existingClient.Achternaam = clienten.Achternaam;
                existingClient.FotoFile = clienten.FotoFile;
                existingClient.Maandag = clienten.Maandag;
                existingClient.Dinsdag = clienten.Dinsdag;
                existingClient.Woensdag = clienten.Woensdag;
                existingClient.Donderdag = clienten.Donderdag;
                existingClient.Vrijdag = clienten.Vrijdag;
                existingClient.Zaterdag = clienten.Zaterdag;
                existingClient.Groepskleur = clienten.Groepskleur;

                Console.WriteLine($"Edit - Maandag: {existingClient.Maandag}, Dinsdag: {existingClient.Dinsdag}");

                _context.Update(existingClient);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
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
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Edit: {ex.Message}");
                ViewBag.ErrorMessage = ex.Message;
            }

            return View(clienten);
        }

        private bool ClientenExists(int id)
        {
            return _context.clienten.Any(e => e.persid == id);
        }

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
    }
}