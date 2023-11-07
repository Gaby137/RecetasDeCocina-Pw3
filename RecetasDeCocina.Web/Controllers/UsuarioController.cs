using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using RecetasDeCocina.Data.Models;
using RecetasDeCocina.Data.Repositories;
using RecetasDeCocina.Web.Models;

namespace RecetasDeCocina.Web.Controllers;

public class UsuarioController : Controller
{
    private IUsuarioCollection db = new UsuarioCollection();
    private IRecetaCollection recetaCollection = new RecetaCollection();
    private IIngredienteCollection ingredientesCo = new IngredienteCollection();
    private IPreferenciaCollection preferenciasCo = new PreferenciaCollection();

    public ActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            // Buscar al usuario por su correo electrónico en la base de datos
            var usuario = db.BuscarPorCorreo(model.Correo);
            if (usuario != null && usuario.Contrasena == model.Contrasena)
            {
                // Autenticación exitosa, establecer una sesión de usuario (puedes usar cookies, por ejemplo)
                // Aquí deberías implementar la lógica para establecer la sesión de usuario
                HttpContext.Session.SetString("UserId", usuario.Id.ToString());
                // Redirigir al usuario a la página de inicio
                return RedirectToAction("Listar", "Receta");
            }
            else
            {
                // Autenticación fallida, mostrar un mensaje de error
                ModelState.AddModelError(string.Empty, "Error de inicio de sesión. Verifica tus credenciales.");
            }
        }

        // Si la autenticación falla, vuelve a mostrar la vista de inicio de sesión con mensajes de error
        return View(model);
    }

    public ActionResult Crear()
    {
        List<Ingrediente> ingredientesDisponibles = ingredientesCo.Listar();
        ViewBag.IngredientesDisponibles = ingredientesDisponibles;
        List<Preferencia> preferenciasAlimentarias = preferenciasCo.Listar();
        ViewBag.preferenciasAlimentarias = preferenciasAlimentarias;
        return View(new Usuario());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Crear(Usuario usuario, string[] ids, string[] idsPreferencias)
    {
        try
        {
            usuario.AlimentosAlergicos = new List<Ingrediente>();
            usuario.PreferenciasAlimentarias = new List<Preferencia>();

            foreach (var id in ids)
            {
                Ingrediente ingrediente = ingredientesCo.BuscarIngredienteConId(ObjectId.Parse(id));
                usuario.AlimentosAlergicos.Add(ingrediente);
            }

            foreach (var id in idsPreferencias)
            {
                Preferencia preferencia = preferenciasCo.BuscarPreferenciaConId(ObjectId.Parse(id));
                usuario.PreferenciasAlimentarias.Add(preferencia);
            }

            db.Crear(usuario); // Supongamos que tienes un método "Crear" en tu base de datos que agrega un usuario a MongoDB
            return RedirectToAction(nameof(Login));
        }
        catch
        {
            return View();
        }
    }
    
    [HttpPost]
    public ActionResult GuardarRecetaFav(string idReceta)
    {
        var idUsuario = HttpContext.Session.GetString("UserId");

        if (idUsuario != null)
        {         
                ObjectId recetaId = ObjectId.Parse(idReceta);

                ObjectId usuarioId = ObjectId.Parse(idUsuario);
                db.GuardarRecetaFav(usuarioId, recetaId);
           
                return RedirectToAction("MisRecetasFavoritas"); 
        }
        return RedirectToAction("MisRecetasFavoritas"); 
    }

    public ActionResult MisRecetasFavoritas()
    {
        var idUsuario = HttpContext.Session.GetString("UserId");

        if (idUsuario != null)
        {
            ObjectId usuarioId = ObjectId.Parse(idUsuario);
            var usuario = db.ObtenerUsuarioPorId(usuarioId);
            return View(usuario.RecetasFavoritas);
        }

        return RedirectToAction("Login"); 
    }    
}