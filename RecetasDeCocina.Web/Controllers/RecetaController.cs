using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using RecetasDeCocina.Data.Models;
using RecetasDeCocina.Data.Repositories;

namespace RecetasDeCocina.Web.Controllers;

public class RecetaController : Controller
{
    private IRecetaCollection db = new RecetaCollection();
    private IIngredienteCollection ingredientesCo = new IngredienteCollection();

    // Propiedad para almacenar temporalmente los ingredientes seleccionados
    private List<Ingrediente> ingredientesSeleccionados = new List<Ingrediente>();


    public ActionResult Crear()
    {
        // Obtén la lista de ingredientes disponibles
        List<Ingrediente> ingredientesDisponibles = ingredientesCo.Listar();

        // Pasa la lista de ingredientes disponibles a la vista utilizando ViewBag
        ViewBag.IngredientesDisponibles = ingredientesDisponibles;

        return View(new Receta());
    }

    /*[HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Crear(Receta receta)
    {
        try
        {
            if (ModelState.IsValid)
            {
                db.Crear(receta); 
                return RedirectToAction(nameof(Listar));
            }
            return View(receta);
        }
        catch
        {
            return View();
        }
    } */

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Crear(Receta receta, string[] ListarIngredientes)
    {
        try
        {
            if (ModelState.IsValid)
            {
                // Obtén la lista de ingredientes disponibles
                List<Ingrediente> ingredientesDisponibles = ingredientesCo.Listar();

                // Pasa la lista de ingredientes disponibles a la vista utilizando ViewBag
                ViewBag.IngredientesDisponibles = ingredientesDisponibles;

                // Construir la lista de ingredientes seleccionados
                //receta.ListarIngredientes = new List<Ingrediente>();
                if (ListarIngredientes != null)
                {
                    foreach (var ingredienteId in ListarIngredientes)
                    {
                        ObjectId objectId;
                        if (ObjectId.TryParse(ingredienteId, out objectId))
                        {
                            Ingrediente ingredienteExistente = ingredientesCo.BuscarIngredienteConId(objectId);
                            if (ingredienteExistente != null)
                            {
                                receta.ListarIngredientes.Add(ingredienteExistente);
                            }
                        }
                    }
                }

                db.Crear(receta);
                return RedirectToAction(nameof(Listar));
            }
            return View(receta);
        }
        catch
        {
            return View();
        }
    }


    /*
    public ActionResult Listar()
    {
        // Obtén la lista de ingredientes disponibles
        List<Ingrediente> ingredientesDisponibles = ingredientesCo.Listar();
        // Pasa la lista de ingredientes disponibles a la vista utilizando ViewBag
        ViewBag.IngredientesDisponibles = ingredientesDisponibles;
        return View(db.Listar());
    }
    */
    public ActionResult Listar()
    {
        List<Receta> recetas = db.Listar();
        return View(recetas);
    } 


   







}





