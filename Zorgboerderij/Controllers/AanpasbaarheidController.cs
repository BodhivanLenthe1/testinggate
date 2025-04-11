using System;
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
                StartTime = new TimeSpan(9, 0, 0),
                EndTime = new TimeSpan(17, 0, 0),
                ExistingLogoUrl = "/images/logo.png"
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

            if (model.LogoFile != null && model.LogoFile.Length > 0)
            {
                var fileName = Path.GetFileName(model.LogoFile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.LogoFile.CopyToAsync(stream);
                }

                model.ExistingLogoUrl = "/images/" + fileName;
            }

            TempData["Message"] = "Instellingen opgeslagen!";
            return RedirectToAction("Index");
        }
    }
}
