using RecetasDeCocina.Data.Models;
using RecetasDeCocina.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RecetasDeCocina.Logica.Servicios;

public interface IRecetasPersonalizadasServicio
{
    List<Receta> FiltrarRecetasPersonalizadas(Usuario usuario, List<Receta> recetas);

    bool CumplePreferenciasYAlergias(Usuario usuario, Receta receta);
}

public class RecetasPersonalizadasServicio : IRecetasPersonalizadasServicio
{

   /* private  MongoDBRepository _context;

    public RecetasPersonalizadasServicio(MongoDBRepository context)
    {
        _context = context;
    } */

    public bool CumplePreferenciasYAlergias(Usuario usuario, Receta receta)
    {
       // Definir reglas y verificar si la receta cumple con cada regla
    if (!CumpleReglaVegetariana(usuario, receta))
        {
            return false;
        }
        if (!CumpleReglaVegano(usuario, receta))
        {
            return false;
        }
        if (!CumpleReglaEquilibrada(usuario, receta))
        {
            return false;
        }
        if (!CumpleReglaSinGluten(usuario, receta))
        {
            return false;
        }
        if (!CumpleReglaSinLactosa(usuario, receta))
        {
            return false;
        }

        if (!CumpleReglaSinMani(usuario, receta))
        {
            return false;
        }
        if (!CumpleReglaSinMariscos(usuario, receta))
        {
            return false;
        }
        if (!CumpleReglaSinLeche(usuario, receta))
        {
            return false;
        }

        // Agregar más reglas según las preferencias y alergias

        return true; // Si la receta cumple con todas las reglas
    }

    public List<Receta> FiltrarRecetasPersonalizadas(Usuario usuario, List<Receta> recetas)
    {
        // Lógica de filtrado basada en las preferencias y alergias del usuario
        var recetasFiltradas = new List<Receta>();
        foreach (var receta in recetas)
        {
            if (CumplePreferenciasYAlergias(usuario, receta))
            {
                recetasFiltradas.Add(receta);
            }
        }
        return recetasFiltradas;
    }

    private bool CumpleReglaSinMani(Usuario usuario, Receta receta)
    {

        if (usuario.AlimentosAlergicos == AlimentosAlergicos.MANI)
        {

            if (receta.ListarIngredientes.Any(ingrediente => ingrediente.NombreDelIngrediente.Contains("mani") ||
                                                           ingrediente.Descripcion.Contains("mani")))
            {

                return false;
            }
        }

        return true;
    }


    private bool CumpleReglaSinLeche(Usuario usuario, Receta receta)
    {
        if (usuario.AlimentosAlergicos == AlimentosAlergicos.LECHE)
        {

            if (receta.ListarIngredientes.Any(ingrediente => ingrediente.NombreDelIngrediente.Contains("leche") ||
                                                           ingrediente.Descripcion.Contains("leche")))
            {

                return false;
            }
        }

        return true;
    }

    private bool CumpleReglaSinMariscos(Usuario usuario, Receta receta)
    {
        if (usuario.AlimentosAlergicos == AlimentosAlergicos.MARISCOS)
        {

            if (receta.ListarIngredientes.Any(ingrediente => ingrediente.NombreDelIngrediente.Contains("mariscos") ||
                                                           ingrediente.Descripcion.Contains("mariscos")))
            {

                return false;
            }
        }

        return true;
    }

    private bool CumpleReglaSinLactosa(Usuario usuario, Receta receta)
    {
        
        if (usuario.PreferenciasAlimentarias == PreferenciasAlimentarias.SIN_LACTOSA)
        {
           
            if (receta.ListarIngredientes.Any(ingrediente => ingrediente.NombreDelIngrediente.Contains("lactosa") ||
                                                           ingrediente.Descripcion.Contains("lactosa")))
            {
                
                return false;
            }
        }

  
        return true;
    }

    private bool CumpleReglaSinGluten(Usuario usuario, Receta receta)
    {
        if (usuario.PreferenciasAlimentarias == PreferenciasAlimentarias.SIN_GLUTEN)
        {

            if (receta.ListarIngredientes.Any(ingrediente => ingrediente.NombreDelIngrediente.Contains("gluten") ||
                                                           ingrediente.Descripcion.Contains("gluten")))
            {

                return false;
            }
        }


        return true;
    }

    private bool CumpleReglaEquilibrada(Usuario usuario, Receta receta)
    {
        if (usuario.PreferenciasAlimentarias == PreferenciasAlimentarias.EQUILIBRADA)
        {

            if (receta.ListarIngredientes.Any(ingrediente => ingrediente.NombreDelIngrediente.Contains("equilibrada") ||
                                                           ingrediente.Descripcion.Contains("equilibrada")))
            {

                return false;
            }
        }


        return true;
    }

    private bool CumpleReglaVegano(Usuario usuario, Receta receta)
    {
        if (usuario.PreferenciasAlimentarias == PreferenciasAlimentarias.VEGANO)
        {

            if (receta.ListarIngredientes.Any(ingrediente => ingrediente.NombreDelIngrediente.Contains("vegano") ||
                                                           ingrediente.Descripcion.Contains("vegano")))
            {

                return false;
            }
        }


        return true;
    }


    private bool CumpleReglaVegetariana(Usuario usuario, Receta receta)
    {
        // Verificar si el usuario es vegetariano
        if (usuario.PreferenciasAlimentarias == PreferenciasAlimentarias.VEGETARIANO)
        {
            // Verificar si la receta contiene carne
            if (receta.ListarIngredientes.Any(ingrediente => ingrediente.NombreDelIngrediente.Contains("carne") ||
                                                           ingrediente.Descripcion.Contains("carne")))
            {
                // La receta contiene carne y el usuario es vegetariano, no cumple con la regla
                return false;
            }
        }

        // La receta cumple con la regla o el usuario no es vegetariano
        return true;
    }






}
