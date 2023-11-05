using MongoDB.Bson;
using MongoDB.Driver;
using RecetasDeCocina.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecetasDeCocina.Data.Repositories;

public interface IRecetaCollection {
    void Crear(Receta receta);

    List<Receta> Listar();
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

    List<Receta> IRecetaCollection.Listar()
    {
        var recetas = Collection.Find(new BsonDocument()).ToList();
        return recetas;
    }
}
