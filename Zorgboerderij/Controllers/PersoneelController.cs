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
        public async Task<IActionResult> Create([Bind("Voornaam,Achternaam,Groepskleur")] Personeel personeel, IFormFile Foto)
        {
            personeel.Maandag = Request.Form["Maandag"];
            personeel.Dinsdag = Request.Form["Dinsdag"];
            personeel.Woensdag = Request.Form["Woensdag"];
            personeel.Donderdag = Request.Form["Donderdag"];
            personeel.Vrijdag = Request.Form["Vrijdag"];
            personeel.Zaterdag = Request.Form["Zaterdag"];

            if (Foto != null && Foto.Length > 0)
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/personeel");
                Directory.CreateDirectory(uploadsFolder);

                string uniqueFileName = Guid.NewGuid() + "_" + Foto.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await Foto.CopyToAsync(stream);
                }

                personeel.FotoFile = uniqueFileName;
            }

            _context.Add(personeel);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
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
        public async Task<IActionResult> Edit(int id, [Bind("persid,Voornaam,Achternaam,FotoFile,Maandag,Dinsdag,Woensdag,Donderdag,Vrijdag,Zaterdag,Groepskleur")] Personeel personeel, IFormFile Foto)
        {
            if (id != personeel.persid)
                return NotFound();

            try
            {
                var existing = await _context.personeel.FindAsync(id);
                if (existing == null)
                    return NotFound();

                // Verwijder oude foto bij nieuwe upload
                if (Foto != null && Foto.Length > 0)
                {
                    if (!string.IsNullOrEmpty(existing.FotoFile))
                    {
                        var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/personeel", existing.FotoFile);
                        if (System.IO.File.Exists(oldPath))
                            System.IO.File.Delete(oldPath);
                    }

                    string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/personeel");
                    Directory.CreateDirectory(uploadsFolder);

                    string uniqueFileName = Guid.NewGuid() + "_" + Foto.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await Foto.CopyToAsync(stream);
                    }

                    existing.FotoFile = uniqueFileName;
                }

                // Update velden
                existing.Voornaam = personeel.Voornaam;
                existing.Achternaam = personeel.Achternaam;
                existing.Maandag = personeel.Maandag;
                existing.Dinsdag = personeel.Dinsdag;
                existing.Woensdag = personeel.Woensdag;
                existing.Donderdag = personeel.Donderdag;
                existing.Vrijdag = personeel.Vrijdag;
                existing.Zaterdag = personeel.Zaterdag;
                existing.Groepskleur = personeel.Groepskleur;

                _context.Update(existing);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fout bij Edit Personeel: " + ex.Message);
                return View(personeel);
            }
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
                if (!string.IsNullOrEmpty(personeel.FotoFile))
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/personeel", personeel.FotoFile);
                    if (System.IO.File.Exists(path))
                        System.IO.File.Delete(path);
                }

                _context.personeel.Remove(personeel);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }


        private bool PersoneelExists(int id)
        {
            return _context.personeel.Any(e => e.persid == id);
        }
    }
}
