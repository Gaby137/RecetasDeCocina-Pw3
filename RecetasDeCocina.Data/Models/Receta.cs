using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecetasDeCocina.Data.Models;

public  class Receta
{
    [BsonId]
    public ObjectId Id { get; set; }

    public string Titulo { get; set; }
    public string Descripcion { get; set; }
    public List<Ingrediente> ListarIngredientes { get; set; }

}


//_id": ObjectId("recipe_id"),
//"title": "Título de la receta",
//"description": "Descripción de la receta",
//"ingredients": [ { "name": "nombre_del_ingrediente", "quantity": 200, "unit": "gramos" }, // Otros ingredientes ], "preparation_steps": [ "Paso 1:...", "Paso 2:...", // Otros pasos ], "category": "desayuno", "preparation_time": 30, "difficulty": "fácil" // Otros campos }