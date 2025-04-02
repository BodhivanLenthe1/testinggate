using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Zorgboerderij.Entities;

// passwords all accounts except bodhis/qutaibas = Test123!

namespace Zorgboerderij.Controllers
{
    public class ToegangController : Controller
    {
        private readonly AppDbContext _context;

        public ToegangController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize]
        // GET: Instellingen
        public async Task<IActionResult> Index()
        {
            return View(await _context.userAccounts.ToListAsync());
        }

        [Authorize]
        // GET: Instellingen/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userAccount = await _context.userAccounts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userAccount == null)
            {
                return NotFound();
            }

            return View(userAccount);
        }

        [Authorize]
        // GET: Instellingen/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Instellingen/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OrgId,Voornaam,Achternaam,Email,Gebruikersnaam,Wachtwoord")] UserAccount userAccount)
        {
            var existingUser = await _context.userAccounts
                .FirstOrDefaultAsync(u => u.Email == userAccount.Email || u.Gebruikersnaam == userAccount.Gebruikersnaam);

            if (existingUser != null)
            {
                ModelState.AddModelError(string.Empty, "Een gebruiker met deze email/gebruikersnaam bestaat al.");
            }

            if (ModelState.IsValid)
            {
                // Wachtwoord hashen met BCrypt
                userAccount.Wachtwoord = BCrypt.Net.BCrypt.HashPassword(userAccount.Wachtwoord);

                _context.Add(userAccount);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(userAccount);
        }


        [Authorize]
        // GET: Instellingen/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userAccount = await _context.userAccounts.FindAsync(id);
            if (userAccount == null)
            {
                return NotFound();
            }
            return View(userAccount);
        }

        [Authorize]
        // POST: Instellingen/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OrgId,Voornaam,Achternaam,Email,Gebruikersnaam,Wachtwoord")] UserAccount userAccount)
        {
            if (id != userAccount.Id)
            {
                return NotFound();
            }

            // Zoek de bestaande entiteit
            var existingUser = await _context.userAccounts.FindAsync(id);
            if (existingUser == null)
            {
                return NotFound();
            }

            // Als het wachtwoord gewijzigd is, moet het opnieuw worden gehasht
            if (existingUser.Wachtwoord != userAccount.Wachtwoord)
            {
                userAccount.Wachtwoord = BCrypt.Net.BCrypt.HashPassword(userAccount.Wachtwoord);
            }

            // Markeer de bestaande entiteit als gewijzigd
            _context.Entry(existingUser).CurrentValues.SetValues(userAccount);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserAccountExists(userAccount.Id))
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

        [Authorize]
        // GET: Instellingen/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userAccount = await _context.userAccounts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userAccount == null)
            {
                return NotFound();
            }

            return View(userAccount);
        }

        // POST: Instellingen/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userAccount = await _context.userAccounts.FindAsync(id);
            if (userAccount != null)
            {
                _context.userAccounts.Remove(userAccount);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserAccountExists(int id)
        {
            return _context.userAccounts.Any(e => e.Id == id);
        }
    }
}
