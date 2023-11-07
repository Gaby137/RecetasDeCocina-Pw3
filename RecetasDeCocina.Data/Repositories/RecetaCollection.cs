using MongoDB.Bson;
using MongoDB.Driver;
using RecetasDeCocina.Data.Models;

namespace RecetasDeCocina.Data.Repositories;

public interface IRecetaCollection
{
    void Crear(Receta receta);
    List<Receta> Listar();
    List<Receta> Filtrar(TipoDePlato? tipoDePlato, PaisDeOrigen? paisDeOrigen, Dificultad? dificultad, List<Ingrediente>? ingredientes, List<Preferencia>? preferencias);
    Receta BuscarRecetaPorId(ObjectId id);
    bool RecetaContieneIngrediente(Receta? receta, List<Ingrediente>? ingredientes);
}

public class RecetaCollection : IRecetaCollection
{
    internal MongoDBRepository _repository = new MongoDBRepository();
    private IMongoCollection<Receta> Collection;

    public RecetaCollection()
    {
        Collection = _repository.db.GetCollection<Receta>("Recetas");
    }

    public void Crear(Receta receta)
    {
        Collection.InsertOneAsync(receta);
    }

    public List<Receta> Listar()
    {
        return Collection.Find(new BsonDocument()).ToList();
    }

    public Receta BuscarRecetaPorId(ObjectId id)
    {
        return Collection.Find(r => r.Id == id).FirstOrDefault();
    }

    public List<Receta> Filtrar(TipoDePlato? tipoDePlato, PaisDeOrigen? paisDeOrigen, Dificultad? dificultad, List<Ingrediente>? ingredientes, List<Preferencia>? preferencias)
    {
        var recetas = Listar();

        if (tipoDePlato.HasValue)
        {
            recetas = recetas.Where(r => r.TipoDePlato == tipoDePlato.Value).ToList();
        }

        if (paisDeOrigen.HasValue)
        {
            recetas = recetas.Where(r => r.PaisDeOrigen == paisDeOrigen.Value).ToList();
        }

        if (dificultad.HasValue)
        {
            recetas = recetas.Where(r => r.Dificultad == dificultad.Value).ToList();
        }

        if (ingredientes != null && ingredientes.Count > 0)
        {
            recetas = recetas.Where(r => ingredientes.All(i => r.ListaIngredientes.Any(li => li.Id == i.Id))).ToList();
        }

        if (preferencias != null && preferencias.Count > 0)
        {
            recetas = recetas.Where(r => preferencias.All(p => r.PreferenciasAlimentarias.Any(pa => pa.Id == p.Id))).ToList();
        }

        return recetas;
    }

    public bool RecetaContieneIngrediente(Receta? receta, List<Ingrediente>? ingredientes)
    {
        return receta.ListaIngredientes.Any(ingrediente => ingredientes.Any(i => i.Id == ingrediente.Id));
    }
}