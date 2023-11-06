using MongoDB.Bson;
using MongoDB.Driver;
using RecetasDeCocina.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecetasDeCocina.Data.Repositories;

public interface IUsuarioCollection
{
    void Crear(Usuario usuario);

    List<Usuario> Listar();

    Usuario ObtenerUsuarioPorId(ObjectId id);
    Usuario BuscarPorCorreo(string correo);

    void GuardarRecetaFav(ObjectId usuarioId, ObjectId recetaId);
}


public class UsuarioCollection : IUsuarioCollection
{
    internal MongoDBRepository _repository = new MongoDBRepository();
    private IMongoCollection<Usuario> Collection;

    private IRecetaCollection _recetaCollection = new RecetaCollection();

    public UsuarioCollection()
    {
        Collection = _repository.db.GetCollection<Usuario>("Usuarios");
    }

    public void Crear(Usuario usuario)
    {
        Collection.InsertOneAsync(usuario);
    }

    public List<Usuario> Listar()
    {
        throw new NotImplementedException();
    }

    public Usuario ObtenerUsuarioPorId(ObjectId id)
    {
        var filter = Builders<Usuario>.Filter.Eq("_id", id);
        var usuarioEncontrado = Collection.Find(filter).FirstOrDefault();

        return usuarioEncontrado;
    }
    public Usuario BuscarPorCorreo(string correo)
    {
        var filter = Builders<Usuario>.Filter.Eq(u => u.Correo, correo);

        // Realizar la consulta en la base de datos
        var usuario = Collection.Find(filter).FirstOrDefault();

        return usuario;
    }

    public void GuardarRecetaFav(ObjectId usuarioId, ObjectId recetaId)
    {
        var usuario = ObtenerUsuarioPorId(usuarioId);
        var receta = _recetaCollection.BuscarRecetaPorId(recetaId);

        if (usuario != null && receta != null)
        {
            Collection.UpdateOne(
                Builders<Usuario>.Filter.Eq(u => u.Id, usuarioId),
                Builders<Usuario>.Update.AddToSet(u => u.RecetasFavoritas, recetaId)
            );
        }
    }
}