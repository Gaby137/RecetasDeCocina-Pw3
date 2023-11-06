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

    public ActionResult Crear()
    {
        List<Ingrediente> ingredientesDisponibles = ingredientesCo.Listar();

        ViewBag.IngredientesDisponibles = ingredientesDisponibles;
        
        return View(new Receta());
    }


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
        
        db.Crear(receta);
        
        return RedirectToAction(nameof(Listar));
    }

    public ActionResult Listar(TipoDePlato? tipoDePlato, PaisDeOrigen? paisDeOrigen, Dificultad? dificultad)
    {
        List<Ingrediente> ingredientesDisponibles = ingredientesCo.Listar();  
        ViewBag.IngredientesDisponibles = ingredientesDisponibles;
        AgregarFiltrosAlViewBag(tipoDePlato, paisDeOrigen, dificultad);

        var recetasFiltradas = db.Filtrar(tipoDePlato, paisDeOrigen, dificultad);

        return View(recetasFiltradas);
    }
    */
    public ActionResult Listar()
    {
        List<Receta> recetas = db.Listar();
        return View(recetas);
    } 

    private void AgregarFiltrosAlViewBag(TipoDePlato? tipoDePlato, PaisDeOrigen? paisDeOrigen, Dificultad? dificultad)
    {
        ViewBag.Tipos = Enum.GetValues(typeof(TipoDePlato)).Cast<TipoDePlato>().ToList();
        ViewBag.TipoSeleccionado = tipoDePlato;
        ViewBag.Paises = Enum.GetValues(typeof(PaisDeOrigen)).Cast<PaisDeOrigen>().ToList();
        ViewBag.PaisSeleccionado = paisDeOrigen;
        ViewBag.Dificultades = Enum.GetValues(typeof(Dificultad)).Cast<Dificultad>().ToList();
        ViewBag.DificultadSeleccionada = dificultad;
    }

}
