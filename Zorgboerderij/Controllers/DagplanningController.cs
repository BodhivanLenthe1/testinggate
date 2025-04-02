using Microsoft.AspNetCore.Mvc;

public class DagplanningController : Controller
{
    public IActionResult Dagkeuze(string dag)
    {
        if (string.IsNullOrEmpty(dag))
        {
            dag = "Onbekend";
        }
        ViewData["GekozenDag"] = dag;
        return View();
    }
}
