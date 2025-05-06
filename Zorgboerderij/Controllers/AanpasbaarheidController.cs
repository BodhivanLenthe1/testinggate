using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Zorgboerderij.Models;

namespace Zorgboerderij.Controllers
{
    public class AanpasbaarheidController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AanpasbaarheidController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var aanpasbaarheid = _context.Aanpasbaarheid.FirstOrDefault();
            var model = new AanpasbaarheidViewModel
            {
                ExistingLogo = aanpasbaarheid?.Logo ?? "/images/LogoBoerderij.png"
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(AanpasbaarheidViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var aanpasbaarheid = _context.Aanpasbaarheid.FirstOrDefault() ?? new Aanpasbaarheid();

            if (model.LogoFile != null && model.LogoFile.Length > 0)
            {
                var fileName = Path.GetFileName(model.LogoFile.FileName);
                var fileExtension = Path.GetExtension(fileName).ToLower();

                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                if (!allowedExtensions.Contains(fileExtension))
                {
                    ModelState.AddModelError("LogoFile", "Alleen .jpg, .jpeg, .png of .gif bestanden zijn toegestaan.");
                    return View(model);
                }

                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                var newFilePath = Path.Combine(uploadsFolder, fileName);

                // Oud logo verwijderen
                if (!string.IsNullOrEmpty(aanpasbaarheid.Logo))
                {
                    var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", aanpasbaarheid.Logo.TrimStart('/').Replace("/", Path.DirectorySeparatorChar.ToString()));
                    if (System.IO.File.Exists(oldFilePath) && oldFilePath != newFilePath)
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }

                using (var stream = new FileStream(newFilePath, FileMode.Create))
                {
                    await model.LogoFile.CopyToAsync(stream);
                }

                aanpasbaarheid.Logo = "/images/" + fileName;
                model.ExistingLogo = aanpasbaarheid.Logo;

                if (aanpasbaarheid.Id == 0)
                    _context.Aanpasbaarheid.Add(aanpasbaarheid);
                else
                    _context.Aanpasbaarheid.Update(aanpasbaarheid);

                await _context.SaveChangesAsync();
            }

            ViewBag.Refresh = true;
            TempData["Message"] = "Instellingen opgeslagen!";
            return View(model);
        }
    }
}
