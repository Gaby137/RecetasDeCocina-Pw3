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

    public ActionResult GenerarRecetasPersonalizadas()
    {
        var idUsuario = HttpContext.Session.GetString("UserId");

        if (idUsuario != null)
        {
            //Lo convierto a objectId
            ObjectId id = ObjectId.Parse(idUsuario);

            // Busca al usuario en la base de datos
            var usuario = usuarioCo.ObtenerUsuarioPorId(id);

            // Si el usuario no existe, redirige a la acción del controlador de inicio
            if (usuario == null)
            {
                return RedirectToAction("Login", "Usuario");
            }

            var recetas = db.Listar();

            // Filtra las recetas según las preferencias y alergias del usuario
            var recetasPersonalizadas = _recetasPersonalizadasServicio.FiltrarRecetasPersonalizadas(usuario, recetas);

            // Pasa las recetas filtradas a la vista utilizando ViewBag
            ViewBag.RecetasPersonalizadas = recetasPersonalizadas;

            return View();
        }
        return RedirectToAction("Login", "Usuario");
    }
}
