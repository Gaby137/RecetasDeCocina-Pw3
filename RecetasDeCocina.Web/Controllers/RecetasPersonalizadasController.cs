using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RecetasDeCocina.Data.Models;
using RecetasDeCocina.Data.Repositories;
using RecetasDeCocina.Logica.Servicios;
using RecetasDeCocina.Web.Models;

namespace RecetasDeCocina.Web.Controllers;

public class RecetasPersonalizadasController : Controller
{

    private IRecetasPersonalizadasServicio _recetasPersonalizadasServicio;
    private IRecetaCollection db = new RecetaCollection();
    private IIngredienteCollection ingredientesCo = new IngredienteCollection();
    private IUsuarioCollection usuarioCo = new UsuarioCollection();

   // private readonly UserManager<Usuario> _userManager;

    public RecetasPersonalizadasController(IRecetasPersonalizadasServicio recetasPersonalizadasServicio)
    {
        _recetasPersonalizadasServicio = recetasPersonalizadasServicio;
    }

    /*
       public async Task<ActionResult> GenerarRecetasPersonalizadas()
       {

           // Obtén la id del usuario de la sesión
            int idUsuario = (int)HttpContext.Session.GetInt32("UserId");

            // Busca al usuario en la base de datos
            var usuario = await usuarioCo.ObtenerUsuarioPorId(idUsuario);

           // Si el usuario no existe, redirige a la acción del controlador de inicio
           if (usuario == null)
           {
               return RedirectToAction("Login", "Usuario");
           }

           // Obtén todas las recetas de tu base de datos
           var recetas = db.Listar();

           // Obtén la lista de ingredientes disponibles
           List<Ingrediente> ingredientesDisponibles = ingredientesCo.Listar();

           // Pasa la lista de ingredientes disponibles a la vista utilizando ViewBag
           ViewBag.IngredientesDisponibles = ingredientesDisponibles;

           // Filtra las recetas según las preferencias y alergias del usuario
           var recetasFiltradas = _recetasPersonalizadasServicio.FiltrarRecetasPersonalizadas(usuario, recetas);

           // Pasa las recetas filtradas a la vista utilizando ViewBag
           ViewBag.RecetasPersonalizadas = recetasFiltradas;

           return View();
       } 
      */
    /*
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

     */
}
