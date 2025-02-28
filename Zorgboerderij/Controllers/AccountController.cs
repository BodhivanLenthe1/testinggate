using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Security.Claims;
using Zorgboerderij.Entities;
using Zorgboerderij.Models;

namespace Zorgboerderij.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        [Authorize]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Registration(RegistrationVM model)
        {
            if (ModelState.IsValid)
            {
                UserAccount account = new UserAccount();
                account.OrgId = model.OrgId;
                account.Voornaam = model.Voornaam;
                account.Achternaam = model.Achternaam;
                account.Email = model.Email;
                account.Gebruikersnaam = model.Gebruikersnaam;
                account.Wachtwoord = model.Wachtwoord;

                try
                {
                    _context.userAccounts.Add(account);
                    _context.SaveChanges();

                    ModelState.Clear();
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", "Voer een uniek email of gebruikersnaam in.");
                    return View(model);
                }
                return View("Login");
            }
            return View(model);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginVM model)
        {
            if(ModelState.IsValid)
            {
                var user = _context.userAccounts.Where(x => x.OrgId == model.OrgId && x.Gebruikersnaam == model.Gebruikersnaam && x.Wachtwoord == model.Wachtwoord).FirstOrDefault();
                if(user != null)
                {
                    // Success, Cookie maken
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Email),
                        new Claim("Name", user.Voornaam),
                        new Claim(ClaimTypes.Role, "User"),
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Gebruikersnaam/Wachtwoord zijn incorrect.");
                }
            }
            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        [Authorize]
        public IActionResult SecurePage()
        {
            ViewBag.Name = HttpContext.User.Identity.Name;
            return View();
        }
    }
}
