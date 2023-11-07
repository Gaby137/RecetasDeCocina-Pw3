using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecetasDeCocina.Data.Models
{
    internal class Borrar
    {
        /*
         * 
        public static void Main()
        {
            // Supongamos que tienes un usuario con preferencias y alergias específicas
            var usuario = new Usuario
            {
                NombreDeUsuario = "MiUsuario",
                PreferenciasAlimentarias = PreferenciasAlimentarias.VEGETARIANO,
                AlimentosAlergicos = AlimentosAlergicos.MANI
            };

            // Supongamos que tienes una lista estática de recetas
            var recetas = ObtenerRecetasEjemplo();

            // Filtrar las recetas basadas en las preferencias y alergias del usuario
            var recetasPersonalizadas = FiltrarRecetasPersonalizadas(usuario, recetas);

            // Mostrar las recetas personalizadas
            Console.WriteLine("Recetas personalizadas para " + usuario.NombreDeUsuario + ":");
            foreach (var receta in recetasPersonalizadas)
            {
                Console.WriteLine(receta.Titulo);
            }
        }

        // Función de ejemplo para filtrar recetas personalizadas
        public static List<Receta> FiltrarRecetasPersonalizadas(Usuario usuario, List<Receta> recetas)
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

        // Función de ejemplo para comprobar si una receta cumple con las preferencias y alergias del usuario
        public static bool CumplePreferenciasYAlergias(Usuario usuario, Receta receta)
        {
            // Simulamos una lógica de filtrado aquí
            if (usuario.PreferenciasAlimentarias == PreferenciasAlimentarias.VEGETARIANO &&
                receta.ContieneCarne)
            {
                return false;
            }

            if (usuario.AlimentosAlergicos == AlimentosAlergicos.MANI &&
                receta.ContieneMani)
            {
                return false;
            }

            // Puedes agregar más lógica de filtrado aquí según las preferencias y alergias

            return true;
        }

        // Función de ejemplo para obtener una lista estática de recetas
        public static List<Receta> ObtenerRecetasEjemplo()
        {
            // Aquí puedes definir tus recetas de ejemplo
            var recetas = new List<Receta>
            {
                new Receta { Titulo = "Receta 1", ContieneCarne = false, ContieneMani = false },
                new Receta { Titulo = "Receta 2", ContieneCarne = true, ContieneMani = false },
                new Receta { Titulo = "Receta 3", ContieneCarne = false, ContieneMani = true }
            };
            return recetas;
        }
    }

    public enum PreferenciasAlimentarias
    {
        EQUILIBRADA,
        VEGETARIANO,
        VEGANO,
        SIN_GLUTEN,
        SIN_LACTOSA
    }

    public enum AlimentosAlergicos
    {
        NINGUNO,
        MANI,
        MARISCOS,
        LECHE
    }

    public class Usuario
    {
        public string NombreDeUsuario { get; set; }
        public PreferenciasAlimentarias PreferenciasAlimentarias { get; set; }
        public AlimentosAlergicos AlimentosAlergicos { get; set; }
    }

    public class Receta
    {
        public string Titulo { get; set; }
        public bool ContieneCarne { get; set; }
        public bool ContieneMani { get; set; }
    }
}
 */
    }
}
