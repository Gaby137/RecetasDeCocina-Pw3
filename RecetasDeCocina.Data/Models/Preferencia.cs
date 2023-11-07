using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RecetasDeCocina.Data.Models;

public class Preferencia
{
    [BsonId]
    public ObjectId Id { get; set; }
    public string PreferenciaAlimentaria { get; set; }
}