using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RecetasDeCocina.Data.Models;

public class Ingrediente
{
    [BsonId]
    public ObjectId Id { get; set; }
    public string Nombre { get; set; }
    public string Categoria { get; set; }
    public string Descripcion { get; set; }
    public string Gramos { get; set; }
}