﻿using Microsoft.AspNetCore.Mvc;
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
    public ActionResult Crear(Receta receta, string[] ids)
    {
        receta.ListaIngredientes = new List<Ingrediente>();
        
        foreach (var id in ids)
        {
            Ingrediente ingrediente = ingredientesCo.BuscarIngredienteConId(ObjectId.Parse(id));
            receta.ListaIngredientes.Add(ingrediente);
        }
        
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
