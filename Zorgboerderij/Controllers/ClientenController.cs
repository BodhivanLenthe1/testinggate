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
        public async Task<IActionResult> Create([Bind("Voornaam,Achternaam,Groepskleur")] Clienten clienten, IFormFile Foto)
        {
            clienten.Maandag = Request.Form["Maandag"];
            clienten.Dinsdag = Request.Form["Dinsdag"];
            clienten.Woensdag = Request.Form["Woensdag"];
            clienten.Donderdag = Request.Form["Donderdag"];
            clienten.Vrijdag = Request.Form["Vrijdag"];
            clienten.Zaterdag = Request.Form["Zaterdag"];

            if (Foto != null && Foto.Length > 0)
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/clienten");
                Directory.CreateDirectory(uploadsFolder);

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + Foto.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await Foto.CopyToAsync(fileStream);
                }

                clienten.FotoFile = uniqueFileName;
            }

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
        public async Task<IActionResult> Edit(int id, [Bind("persid,Voornaam,Achternaam,FotoFile,Maandag,Dinsdag,Woensdag,Donderdag,Vrijdag,Zaterdag,Groepskleur")] Clienten clienten, IFormFile Foto)
        {
            if (id != clienten.persid)
                return NotFound();

            try
            {
                var existingClient = await _context.clienten.FindAsync(id);
                if (existingClient == null)
                    return NotFound();

                if (Foto != null && Foto.Length > 0)
                {
                    if (!string.IsNullOrEmpty(existingClient.FotoFile))
                    {
                        var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/clienten", existingClient.FotoFile);
                        if (System.IO.File.Exists(oldPath))
                            System.IO.File.Delete(oldPath);
                    }

                    string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/clienten");
                    Directory.CreateDirectory(uploadsFolder);

                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + Foto.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await Foto.CopyToAsync(fileStream);
                    }

                    existingClient.FotoFile = uniqueFileName;
                }

                existingClient.Voornaam = clienten.Voornaam;
                existingClient.Achternaam = clienten.Achternaam;
                existingClient.Maandag = clienten.Maandag;
                existingClient.Dinsdag = clienten.Dinsdag;
                existingClient.Woensdag = clienten.Woensdag;
                existingClient.Donderdag = clienten.Donderdag;
                existingClient.Vrijdag = clienten.Vrijdag;
                existingClient.Zaterdag = clienten.Zaterdag;
                existingClient.Groepskleur = clienten.Groepskleur;

                _context.Update(existingClient);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Edit: {ex.Message}");
                ViewBag.ErrorMessage = ex.Message;
                return View(clienten);
            }
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
                if (!string.IsNullOrEmpty(clienten.FotoFile))
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/clienten", clienten.FotoFile);
                    if (System.IO.File.Exists(path))
                        System.IO.File.Delete(path);
                }

                _context.clienten.Remove(clienten);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

    }
}