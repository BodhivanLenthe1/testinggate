using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zorgboerderij.Entities;

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
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var users = await _context.userAccounts.ToListAsync();
            var currentUserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (currentUserId != null)
            {
                foreach (var user in users)
                {
                    if (user.Id.ToString() == currentUserId)
                    {
                        user.LaatstIngelogd = DateTime.Now;  
                        _context.Update(user);
                        await _context.SaveChangesAsync();
                    }

                    Console.WriteLine($"Gebruiker: {user.Gebruikersnaam}, Laatst ingelogd: {user.LaatstIngelogd}");
                }
            }
            else
            {
                Console.WriteLine("Geen gebruiker ingelogd.");
            }

            return View(users);
        }


        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var userAccount = await _context.userAccounts.FirstOrDefaultAsync(m => m.Id == id);
            if (userAccount == null)
                return NotFound();

            return View(userAccount);
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OrgId,Voornaam,Achternaam,Email,Gebruikersnaam,Wachtwoord,Toegang,LaatstIngelogd")] UserAccount userAccount)
        {
            var existingUser = await _context.userAccounts
                .FirstOrDefaultAsync(u => u.Email == userAccount.Email || u.Gebruikersnaam == userAccount.Gebruikersnaam);

            if (existingUser != null)
                ModelState.AddModelError(string.Empty, "Een gebruiker met deze email/gebruikersnaam bestaat al.");

            if (ModelState.IsValid)
            {
                userAccount.Wachtwoord = BCrypt.Net.BCrypt.HashPassword(userAccount.Wachtwoord);
                _context.Add(userAccount);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(userAccount);
        }

        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var userAccount = await _context.userAccounts.FindAsync(id);
            if (userAccount == null)
                return NotFound();

            return View(userAccount);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OrgId,Voornaam,Achternaam,Email,Gebruikersnaam,Wachtwoord,Toegang,LaatstIngelogd")] UserAccount userAccount)
        {
            if (id != userAccount.Id)
                return NotFound();

            var existingUser = await _context.userAccounts.FindAsync(id);
            if (existingUser == null)
                return NotFound();

            if (existingUser.Wachtwoord != userAccount.Wachtwoord)
                userAccount.Wachtwoord = BCrypt.Net.BCrypt.HashPassword(userAccount.Wachtwoord);

            _context.Entry(existingUser).CurrentValues.SetValues(userAccount);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserAccountExists(userAccount.Id))
                    return NotFound();
                else
                    throw;
            }

            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var userAccount = await _context.userAccounts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userAccount == null)
                return NotFound();

            return View(userAccount);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userAccount = await _context.userAccounts.FindAsync(id);
            if (userAccount != null)
                _context.userAccounts.Remove(userAccount);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserAccountExists(int id)
        {
            return _context.userAccounts.Any(e => e.Id == id);
        }
    }
}