using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace RecetasDeCocina.Data.Models;

public  class Usuario 
{
    [BsonId]
    public ObjectId Id { get; set; }
    [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre de usuario debe tener entre 3 y 50 caracteres.")]
    public string NombreDeUsuario { get; set; }
    [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
    [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
    public string Correo { get; set; }
    [Required(ErrorMessage = "La contraseña es obligatoria.")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
    public string Contrasena { get; set; }
    public List<Receta> RecetasFavoritas { get; set; }

    public List<Preferencia> PreferenciasAlimentarias {  get; set; }
    public List<Ingrediente> AlimentosAlergicos { get; set; }

    public Usuario()
    {
        RecetasFavoritas = new List<Receta>();
    }
}
