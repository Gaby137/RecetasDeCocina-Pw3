using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
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


    public RecetasPersonalizadasController(IRecetasPersonalizadasServicio recetasPersonalizadasServicio)
    {
        _recetasPersonalizadasServicio = recetasPersonalizadasServicio;
    }

    
       public async Task<ActionResult> GenerarRecetasPersonalizadas()
       {

            // Obtén la id del usuario de la sesión
            var usuarioId = HttpContext.Session.GetString("UserId");
            //Lo convierto a objectId
             ObjectId idUsuario = ObjectId.Parse(usuarioId);

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
         //  List<Ingrediente> ingredientesDisponibles = ingredientesCo.Listar();

           // Pasa la lista de ingredientes disponibles a la vista utilizando ViewBag
       //    ViewBag.IngredientesDisponibles = ingredientesDisponibles;

           // Filtra las recetas según las preferencias y alergias del usuario
           var recetasFiltradas = _recetasPersonalizadasServicio.FiltrarRecetasPersonalizadas(usuario, recetas);

           // Pasa las recetas filtradas a la vista utilizando ViewBag
           ViewBag.RecetasPersonalizadas = recetasFiltradas;

           return View();
       } 
      
    
   
     
}
