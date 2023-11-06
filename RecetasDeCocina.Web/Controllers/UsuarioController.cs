﻿using Microsoft.AspNetCore.Mvc;
using RecetasDeCocina.Data.Models;
using RecetasDeCocina.Data.Repositories;
using RecetasDeCocina.Web.Models;
using MongoDB.Bson;
using System.Diagnostics;

namespace RecetasDeCocina.Web.Controllers;

public class UsuarioController : Controller
{

    private IUsuarioCollection db = new UsuarioCollection();

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
                return RedirectToAction("Index", "Home");
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

    public ActionResult Details(int id)
    {
        return View();
    }


    public ActionResult Crear()
    {
        return View();
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Crear(Usuario usuario)
    {
        try
        {
            if (ModelState.IsValid)
            {
                db.Crear(usuario); // Supongamos que tienes un método "Crear" en tu base de datos que agrega un usuario a MongoDB
                return RedirectToAction(nameof(Login));
            }
            return View(usuario);
        }
        catch
        {
            return View();
        }
    }


    public ActionResult Edit(int id)
    {
        return View();
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(int id, IFormCollection collection)
    {
        try
        {
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }


    public ActionResult Delete(int id)
    {
        return View();
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Delete(int id, IFormCollection collection)
    {
        try
        {
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }
    /* 
    [HttpPost]
    public ActionResult GuardarRecetaFav(string idReceta)
    {
        Debug.WriteLine($"idReceta: {idReceta}");


        var usuarioId = HttpContext.Session.GetString("UserId");

        Debug.WriteLine($"usuarioId: {usuarioId}");


        if (usuarioId != null)
        {
            if (ObjectId.TryParse(idReceta, out ObjectId recetaId))
            {
                ObjectId idUsuario = ObjectId.Parse(usuarioId);
                db.GuardarRecetaFav(idUsuario, recetaId);
            }
            else
            {
                return RedirectToAction("MisRecetasFavoritas");

            }

        }
        return RedirectToAction("MisRecetasFavoritas");

    }

    public ActionResult MisRecetasFavoritas()
    {

        var usuarioId = HttpContext.Session.GetString("UserId");

        if (usuarioId != null)
        {
            ObjectId idUsuario = ObjectId.Parse(usuarioId);
            var usuario = db.ObtenerUsuarioPorId(idUsuario);
            var recetasFavoritas = recetaCollection.ObtenerRecetasPorIds(usuario.RecetasFavoritas);

            return View(recetasFavoritas);
        }

        return View(new List<Receta>());

    }
    */



}