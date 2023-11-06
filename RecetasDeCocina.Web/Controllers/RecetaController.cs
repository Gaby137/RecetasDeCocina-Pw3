﻿using Microsoft.AspNetCore.Mvc;
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

    [HttpPost]
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
    }

    public ActionResult Listar(TipoDePlato? tipoDePlato, PaisDeOrigen? paisDeOrigen, Dificultad? dificultad)
    {
        List<Ingrediente> ingredientesDisponibles = ingredientesCo.Listar();  
        ViewBag.IngredientesDisponibles = ingredientesDisponibles;
        AgregarFiltrosAlViewBag(tipoDePlato, paisDeOrigen, dificultad);

        var recetasFiltradas = db.Filtrar(tipoDePlato, paisDeOrigen, dificultad);

        return View(recetasFiltradas);
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
