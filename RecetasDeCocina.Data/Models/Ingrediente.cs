using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RecetasDeCocina.Data.Models;

public  class Ingrediente
{
    [BsonId]
    public ObjectId Id { get; set; }
    public string NombreDelIngrediente { get; set; }
    public string CategoriaDelIngrediente { get; set; }
    public string Descripcion { get; set; }
    public string Gramos { get; set; }

}
//json { "_id": ObjectId("ingredient_id"),
//"name": "nombre_del_ingrediente",
//"category": "categoría_del_ingrediente",
//"description": "Descripción del ingrediente" // Otros campos }