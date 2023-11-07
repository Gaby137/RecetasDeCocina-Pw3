using Microsoft.AspNetCore.Mvc;
using RecetasDeCocina.Data.Models;
using RecetasDeCocina.Data.Repositories;

namespace RecetasDeCocina.Web.Controllers;

public class IngredienteController : Controller
{
    private IIngredienteCollection db = new IngredienteCollection();

    public ActionResult Crear()
    {
        return View();
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Crear(Ingrediente ingrediente)
    {
        try
        {
            if (ModelState.IsValid)
            {
                db.Crear(ingrediente);
                return RedirectToAction(nameof(Listar));
            }
            return View(ingrediente);
        }
        catch
        {
            return View();
        }
    }


    public ActionResult Listar()
    {
        return View(db.Listar());
    }
}
