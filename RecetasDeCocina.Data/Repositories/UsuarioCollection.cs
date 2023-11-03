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

    Usuario ObtenerUsuarioPorId(string id);
}


public class UsuarioCollection : IUsuarioCollection
{
    internal MongoDBRepository _repository = new MongoDBRepository();
    private IMongoCollection<Usuario> Collection;

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

    public Usuario ObtenerUsuarioPorId(string id)
    {
        throw new NotImplementedException();
    }
}