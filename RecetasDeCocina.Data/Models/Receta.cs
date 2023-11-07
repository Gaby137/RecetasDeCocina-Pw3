using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RecetasDeCocina.Data.Models;

public enum TipoDePlato
{
    DESAYUNO,
    ALMUERZO,
    MERIENDA,
    CENA
}

public enum PaisDeOrigen
{
    ARGENTINA,
    MEXICO,
    PERU,
    COLOMBIA,
    CHILE,
    BRASIL,
    INGLATERRA,
    ESPAÑA,
    ITALIA,
    FRANCIA,
    CHINA,
    JAPON,
    INDIA,
    TAILANDIA
}

public enum Dificultad
{
    FACIL,
    MEDIO,
    DIFICIL
}

public class Receta
{
    [BsonId]
    public ObjectId Id { get; set; }
    public string Titulo { get; set; }
    public string Descripcion { get; set; }
    public List<Ingrediente> ListaIngredientes { get; set; }
    public List<Preferencia> PreferenciasAlimentarias { get; set; }
    public TipoDePlato TipoDePlato { get; set; }
    public PaisDeOrigen PaisDeOrigen { get; set; }
    public Dificultad Dificultad { get; set; }
}
