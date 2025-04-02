using Microsoft.AspNetCore.Mvc;

namespace Zorgboerderij.Controllers
{
    public class DagindelingController : Controller
    {
        public IActionResult Dagindeling()
        {
            return View();
        }
    }
}
