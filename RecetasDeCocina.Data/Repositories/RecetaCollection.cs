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
    private IngredienteCollection _ingredienteCollection;

    public RecetaCollection()
    {
        Collection = _repository.db.GetCollection<Receta>("Recetas");
        _ingredienteCollection = new IngredienteCollection(); // Inicializa _ingredienteCollection
    }


    public void Crear(Receta receta)
    {
        foreach (Ingrediente ingrediente in receta.ListarIngredientes)
        {
            Ingrediente ingredienteExistente = _ingredienteCollection.BuscarIngredienteConId(ingrediente.Id);
            if (ingredienteExistente != null)
            {
                // Agregar el ingrediente existente a la receta
                receta.ListarIngredientes.Add(ingredienteExistente);
            }
        }

        Collection.InsertOneAsync(receta);
    }


    public List<Receta> Listar()
    {
        var projection = Builders<Receta>.Projection
            .Include(r => r.Titulo)
            .Include(r => r.Descripcion)
            .Include(r => r.ListarIngredientes); // Esto incluirá la lista completa de ingredientes

        var recetas = Collection.Find(new BsonDocument())
            .Project<Receta>(projection)
            .ToList();

        return recetas;
    }




}
