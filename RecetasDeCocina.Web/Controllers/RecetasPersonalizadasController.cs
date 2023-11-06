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

    
       public async Task<ActionResult> GenerarRecetasPersonalizadas(TipoDePlato? tipoDePlato, PaisDeOrigen? paisDeOrigen, Dificultad? dificultad)
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

        AgregarFiltrosAlViewBag(tipoDePlato, paisDeOrigen, dificultad);

        // Obtén todas las recetas de tu base de datos
        var recetasFiltradas = db.Filtrar(tipoDePlato, paisDeOrigen, dificultad);


           // Filtra las recetas según las preferencias y alergias del usuario
           var recetasPersonalizadas = _recetasPersonalizadasServicio.FiltrarRecetasPersonalizadas(usuario, recetasFiltradas);

           // Pasa las recetas filtradas a la vista utilizando ViewBag
           ViewBag.RecetasPersonalizadas = recetasPersonalizadas;

           return View();
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
