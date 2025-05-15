using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Zorgboerderij.Models;
using Zorgboerderij.Entities;
using System;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Zorgboerderij.Controllers
{
    public class AanpasbaarheidController(IWebHostEnvironment env, AppDbContext context) : Controller
    {
        private readonly IWebHostEnvironment _env = env;
        private readonly AppDbContext _context = context;

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var entiteit = await _context.Aanpasbaarheid.FirstOrDefaultAsync();
            var model = new AanpasbaarheidViewModel
            {
                ExistingLogo = $"/images/Logo/{entiteit.Logo}"
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(AanpasbaarheidViewModel model)
        {
            Debug.Print("Logo post werkt");

            if (!ModelState.IsValid)
            {
                Console.WriteLine("ModelState is NIET geldig!");
                foreach (var kvp in ModelState)
                {
                    var key = kvp.Key;
                    var errors = kvp.Value.Errors;
                    foreach (var error in errors)
                    {
                        Debug.Print($"Fout bij '{key}': {error.ErrorMessage}");
                    }
                }

                return View(model);
            }

            Debug.Print("Logo post model state IS VALID");

            string newLogoPath = null;

            if (model.LogoFile != null && model.LogoFile.Length > 0)
            {
                var extension = Path.GetExtension(model.LogoFile.FileName).ToLower();
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };

                if (!allowedExtensions.Contains(extension))
                {
                    ModelState.AddModelError("LogoFile", "Alleen .jpg, .jpeg, .png of .gif bestanden zijn toegestaan.");
                    return View(model);
                }

                var uploadsFolder = Path.Combine(_env.WebRootPath, "images", "Logo");

                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var uniqueFileName = $"{Guid.NewGuid()}{extension}";
                var newFilePath = Path.Combine(uploadsFolder, uniqueFileName);

                // Verwijder oude logo als dat bestaat
                var entiteit = await _context.Aanpasbaarheid.FirstOrDefaultAsync();
                if (entiteit != null && !string.IsNullOrEmpty(entiteit.Logo))
                {
                    // Strip "/images/Logo/" van entiteit.Logo om bestandsnaam te krijgen
                    var oldFileName = Path.GetFileName(entiteit.Logo);
                    var oldFilePath = Path.Combine(uploadsFolder, oldFileName);

                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                        Debug.Print($"Verwijderde oud logo: {oldFilePath}");
                    }
                }

                // Opslaan nieuwe logo
                using (var stream = new FileStream(newFilePath, FileMode.Create))
                {
                    await model.LogoFile.CopyToAsync(stream);
                }

                newLogoPath = $"/images/Logo/{uniqueFileName}";
            }

            // Ophalen of aanmaken van de entiteit
            var bestaandeEntiteit = await _context.Aanpasbaarheid.FirstOrDefaultAsync();
            if (bestaandeEntiteit == null)
            {
                bestaandeEntiteit = new Aanpasbaarheid();
                _context.Aanpasbaarheid.Add(bestaandeEntiteit);
            }

            if (!string.IsNullOrEmpty(newLogoPath))
            {
                bestaandeEntiteit.Logo = newLogoPath;
            }

            await _context.SaveChangesAsync();

            model.ExistingLogo = bestaandeEntiteit.Logo;

            TempData["Message"] = "Logo succesvol opgeslagen!";
            return View(model);
        }
    }
}