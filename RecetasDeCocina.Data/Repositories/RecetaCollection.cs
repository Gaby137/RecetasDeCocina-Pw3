﻿using MongoDB.Bson;
using MongoDB.Driver;
using RecetasDeCocina.Data.Models;

namespace RecetasDeCocina.Data.Repositories;

public interface IRecetaCollection {
    void Crear(Receta receta);
    List<Receta> Listar();
    List<Receta> Filtrar(TipoDePlato? tipoDePlato, PaisDeOrigen? paisDeOrigen, Dificultad? dificultad);
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
        var recetas = Collection.Find(new BsonDocument()).ToList();
        return recetas;
    }

    public List<Receta> Filtrar(TipoDePlato? tipoDePlato, PaisDeOrigen? paisDeOrigen, Dificultad? dificultad)
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

        return recetas;
    }
}
