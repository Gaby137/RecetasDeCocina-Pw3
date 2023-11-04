using Microsoft.AspNetCore.Mvc;
using RecetasDeCocina.Data.Models;
using RecetasDeCocina.Data.Repositories;
using RecetasDeCocina.Web.Models;
using System.Diagnostics;

namespace RecetasDeCocina.Web.Controllers;

public class UsuarioController : Controller
{

    private IUsuarioCollection db = new UsuarioCollection();

    public ActionResult Index()
    {
        return View();
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
                return RedirectToAction(nameof(Index));
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




}