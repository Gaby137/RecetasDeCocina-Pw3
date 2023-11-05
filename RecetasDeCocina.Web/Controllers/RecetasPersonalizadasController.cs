using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RecetasDeCocina.Data.Models;
using RecetasDeCocina.Data.Repositories;
using RecetasDeCocina.Logica.Servicios;

namespace RecetasDeCocina.Web.Controllers;

public class RecetasPersonalizadasController : Controller
{

    private IRecetasPersonalizadasServicio _recetasPersonalizadasServicio;
    private IRecetaCollection db = new RecetaCollection();
    private IIngredienteCollection ingredientesCo = new IngredienteCollection();

    private readonly UserManager<Usuario> _userManager;

    public RecetasPersonalizadasController(IRecetasPersonalizadasServicio recetasPersonalizadasServicio, UserManager<Usuario> userManager)
    {
        _recetasPersonalizadasServicio = recetasPersonalizadasServicio;
        _userManager = userManager;
    }

    public async Task<ActionResult> GenerarRecetasPersonalizadas()
    {
        var usuario = await _userManager.GetUserAsync(User);
        if (usuario == null)
        {
            // Manejar el caso en que el usuario no está autenticado
            // Redirige a la acción del controlador de inicio (puedes cambiar "Inicio" por el nombre de tu controlador de inicio)
            return RedirectToAction("Login", "Usuario");
        }

        var recetas = db.Listar(); // Obtén todas las recetas de tu base de datos
        var recetasFiltradas = _recetasPersonalizadasServicio.FiltrarRecetasPersonalizadas(usuario, recetas);
        // Obtén la lista de ingredientes disponibles
        List<Ingrediente> ingredientesDisponibles = ingredientesCo.Listar();
        // Pasa la lista de ingredientes disponibles a la vista utilizando ViewBag
        ViewBag.IngredientesDisponibles = ingredientesDisponibles;
        ViewBag.RecetasPersonalizadas = recetasFiltradas;

        return View();
    }
   
}
