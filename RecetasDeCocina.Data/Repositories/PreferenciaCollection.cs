using MongoDB.Bson;
using MongoDB.Driver;
using RecetasDeCocina.Data.Models;

namespace RecetasDeCocina.Data.Repositories;

public interface IPreferenciaCollection
{
    Preferencia BuscarPreferenciaConId(ObjectId id);
    List<Preferencia> Listar();
}

public class PreferenciaCollection : IPreferenciaCollection
{
    internal MongoDBRepository _repository = new MongoDBRepository();
    private IMongoCollection<Preferencia> Collection;

    public PreferenciaCollection()
    {
        Collection = _repository.db.GetCollection<Preferencia>("Preferencias");
    }

    public Preferencia BuscarPreferenciaConId(ObjectId id)
    {
        return Collection.Find(p => p.Id == id).FirstOrDefault();
    }

    public List<Preferencia> Listar()
    {
        return Collection.Find(new BsonDocument()).ToList();
    }
}