using Microsoft.AspNetCore.Mvc;

namespace RecetasDeCocina.Web.Controllers;

public class RecetasPersonalizadasController : Controller
{
    //Hacer
    public IActionResult Index()
    {
        return View();
    }
}
