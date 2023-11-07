using RecetasDeCocina.Data.Models;
using RecetasDeCocina.Data.Repositories;

namespace RecetasDeCocina.Logica.Servicios;

public interface IRecetasPersonalizadasServicio
{
    List<Receta> FiltrarRecetasPersonalizadas(Usuario usuario, List<Receta> recetas);
}

public class RecetasPersonalizadasServicio : IRecetasPersonalizadasServicio
{
    private IRecetaCollection db = new RecetaCollection();
    
    private bool CumplePreferencias(List<Preferencia> preferenciasUsuario, List<Preferencia> preferenciasReceta)
    {
        return preferenciasUsuario.Any(preferenciaUsuario => preferenciasReceta.Any(preferenciaReceta => preferenciaReceta.Id == preferenciaUsuario.Id));
    }
    
    public List<Receta> FiltrarRecetasPersonalizadas(Usuario usuario, List<Receta> recetas)
    {
        var recetasFiltradas = new List<Receta>();

        foreach (var receta in recetas)
        {
            if (CumplePreferencias(usuario.PreferenciasAlimentarias, receta.PreferenciasAlimentarias) && !db.RecetaContieneIngrediente(receta, usuario.AlimentosAlergicos))
            {
                recetasFiltradas.Add(receta);
            }
        }

        return recetasFiltradas;
    }
}
