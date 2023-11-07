using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using RecetasDeCocina.Data.Models;
using RecetasDeCocina.Data.Repositories;

namespace RecetasDeCocina.Web.Controllers;

public class RecetaController : Controller
{
    private IRecetaCollection db = new RecetaCollection();
    private IIngredienteCollection ingredientesCo = new IngredienteCollection();
    private IPreferenciaCollection preferenciasCo = new PreferenciaCollection();

    public ActionResult Crear()
    {
        List<Ingrediente> ingredientesDisponibles = ingredientesCo.Listar();
        ViewBag.IngredientesDisponibles = ingredientesDisponibles;
        List<Preferencia> preferenciasAlimentarias = preferenciasCo.Listar();
        ViewBag.preferenciasAlimentarias = preferenciasAlimentarias;

        return View(new Receta());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Crear(Receta receta, string[] ids, string[] idsPreferencias)
    {
        receta.ListaIngredientes = new List<Ingrediente>();
        receta.PreferenciasAlimentarias = new List<Preferencia>();

        foreach (var id in ids)
        {
            Ingrediente ingrediente = ingredientesCo.BuscarIngredienteConId(ObjectId.Parse(id));
            receta.ListaIngredientes.Add(ingrediente);
        }

        foreach (var id in idsPreferencias)
        {
            Preferencia preferencia = preferenciasCo.BuscarPreferenciaConId(ObjectId.Parse(id));
            receta.PreferenciasAlimentarias.Add(preferencia);
        }

        db.Crear(receta);

        return RedirectToAction(nameof(Listar));
    }

    public ActionResult Listar(TipoDePlato? tipoDePlato, PaisDeOrigen? paisDeOrigen, Dificultad? dificultad, string[]? idsIngredientes, string[]? idsPreferencias)
    {
        var idUsuario = HttpContext.Session.GetString("UserId");

        if (idUsuario != null)
        {
            List<Ingrediente> ingredientesDisponibles = ingredientesCo.Listar();
            ViewBag.IngredientesDisponibles = ingredientesDisponibles;
            List<Preferencia> preferenciasAlimentarias = preferenciasCo.Listar();
            ViewBag.PreferenciasAlimentarias = preferenciasAlimentarias;

            List<Ingrediente> ingredientesSeleccionados = new List<Ingrediente>();
            List<Preferencia> preferenciasSeleccionadas = new List<Preferencia>();

            foreach (var id in idsIngredientes)
            {
                Ingrediente ingrediente = ingredientesCo.BuscarIngredienteConId(ObjectId.Parse(id));
                ingredientesSeleccionados.Add(ingrediente);
            }

            foreach (var id in idsPreferencias)
            {
                Preferencia preferencia = preferenciasCo.BuscarPreferenciaConId(ObjectId.Parse(id));
                preferenciasSeleccionadas.Add(preferencia);
            }

            var recetasFiltradas = db.Filtrar(tipoDePlato, paisDeOrigen, dificultad, ingredientesSeleccionados, preferenciasSeleccionadas);
            AgregarFiltrosAlViewBag(tipoDePlato, paisDeOrigen, dificultad, idsIngredientes, idsPreferencias);

            return View(recetasFiltradas);
        }
        return RedirectToAction("Login", "Usuario");
    }

    private void AgregarFiltrosAlViewBag(TipoDePlato? tipoDePlato, PaisDeOrigen? paisDeOrigen, Dificultad? dificultad, string[]? idsIngredientes, string[]? idsPreferencias)
    {      
        ViewBag.Tipos = Enum.GetValues(typeof(TipoDePlato)).Cast<TipoDePlato>().ToList();
        ViewBag.TipoSeleccionado = tipoDePlato;
        ViewBag.Paises = Enum.GetValues(typeof(PaisDeOrigen)).Cast<PaisDeOrigen>().ToList();
        ViewBag.PaisSeleccionado = paisDeOrigen;
        ViewBag.Dificultades = Enum.GetValues(typeof(Dificultad)).Cast<Dificultad>().ToList();
        ViewBag.DificultadSeleccionada = dificultad;
        ViewBag.IngredientesSeleccionados = idsIngredientes;
        ViewBag.PreferenciasSeleccionadas = idsPreferencias;
    }
}