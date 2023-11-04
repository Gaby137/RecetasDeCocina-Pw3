using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecetasDeCocina.Data.Models;

public  class PreferenciaUsuario
{
    [BsonId]
    public ObjectId Id { get; set; }

    public ObjectId IdUsuario { get; set; }
    public List<Ingrediente> IngredientesPreferidos { get; set; }
    public List<Ingrediente> IngredientesDespreciados { get; set; }

}
//json { "_id": ObjectId("preference_id"),
//"user_id": ObjectId("user_id"),
//"liked_ingredients": ["zanahorias", "tomates"],
//"disliked_ingredients": ["brócoli", "pimientos"] }