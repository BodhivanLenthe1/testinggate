using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Zorgboerderij.Models;

namespace Zorgboerderij.Controllers
{
    public class AanpasbaarheidController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var model = new AanpasbaarheidViewModel
            {
                ExistingLogo = "/images/LogoBoerderij.png"
            };

            return View(model);
        }

        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> Index(AanpasbaarheidViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

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

                if (!string.IsNullOrEmpty(model.ExistingLogo))
                {
                    var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", model.ExistingLogo.TrimStart('/').Replace("/", Path.DirectorySeparatorChar.ToString()));
                    if (System.IO.File.Exists(oldFilePath) && oldFilePath != newFilePath)
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }

                
                using (var stream = new FileStream(newFilePath, FileMode.Create))
                {
                    await model.LogoFile.CopyToAsync(stream);
                }

                model.ExistingLogo = "/images/" + fileName;
            }

            ViewBag.Refresh = true; 
            TempData["Message"] = "Instellingen opgeslagen!";
            return View(model);
        }

    }
}