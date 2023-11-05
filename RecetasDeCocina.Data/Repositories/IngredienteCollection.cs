﻿using MongoDB.Bson;
using MongoDB.Driver;
using RecetasDeCocina.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecetasDeCocina.Data.Repositories;

public interface IIngredienteCollection
{
    void Crear(Ingrediente ingrediente);

    Ingrediente BuscarIngredienteConId(object id);

    List<Ingrediente> Listar();
}

public class IngredienteCollection : IIngredienteCollection
{

    internal MongoDBRepository _repository = new MongoDBRepository();
    private IMongoCollection<Ingrediente> Collection;

    public IngredienteCollection()
    {
        Collection = _repository.db.GetCollection<Ingrediente>("Ingredientes");
    }

    public Ingrediente BuscarIngredienteConId(object id)
    {
        var filter = Builders<Ingrediente>.Filter.Eq("_id", id);
        var ingredienteEncontrado = Collection.Find(filter).FirstOrDefault();

        return ingredienteEncontrado;
    }


    public void Crear(Ingrediente ingrediente)
    {
        Collection.InsertOneAsync(ingrediente);
    }

    public List<Ingrediente> Listar()
    {
        var ingredientes = Collection.Find(new BsonDocument()).ToList();
        return ingredientes;
    }
}
