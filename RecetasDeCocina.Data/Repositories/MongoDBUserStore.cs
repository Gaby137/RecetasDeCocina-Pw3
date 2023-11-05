/*
using Microsoft.AspNetCore.Identity;
using MongoDB.Driver;
using RecetasDeCocina.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecetasDeCocina.Data.Repositories;

public class MongoDBUserStore : IUserStore<Usuario>
{
    private readonly MongoDBRepository _repository;

    public MongoDBUserStore(MongoDBRepository repository)
    {
        _repository = repository;
    }

    public async Task<IdentityResult> CreateAsync(Usuario user, CancellationToken cancellationToken)
    {
        // Insertar el usuario en la base de datos MongoDB.
        var collection = _repository.db.GetCollection<Usuario>("Usuarios");
        await collection.InsertOneAsync(user, cancellationToken);

        return IdentityResult.Success;
    }

    public async Task<Usuario> FindByIdAsync(string userId, CancellationToken cancellationToken)
    {
        // Buscar el usuario en la base de datos MongoDB por su ID.
        var collection = await _repository.GetCollectionAsync<Usuario>("Usuarios");
        var user = await collection.FindOneAsync(Builders<Usuario>.Filter.Eq(u => u.Id, userId), cancellationToken);

        return user;
    }
    public void Dispose()
    {
        // Liberar los recursos utilizados por el `MongoDBUserStore`.
    }


    public async Task<Usuario> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
    {
        // Buscar el usuario en la base de datos MongoDB por su nombre de usuario normalizado.
        var collection = _repository.GetCollection<Usuario>("Usuarios");
        var user = await collection.FindOneAsync(Builders<Usuario>.Filter.Eq(u => u.NormalizedUserName, normalizedUserName), cancellationToken);

        return user;
    }

    public async Task<string> GetNormalizedUserNameAsync(Usuario user, CancellationToken cancellationToken)
    {
        // Devolver el nombre de usuario normalizado del usuario.
        return user.NormalizedUserName;
    }

    public async Task<string> GetUserIdAsync(Usuario user, CancellationToken cancellationToken)
    {
        // Devolver el ID del usuario.
        return user.Id;
    }

    public async Task<string> GetUserNameAsync(Usuario user, CancellationToken cancellationToken)
    {
        // Devolver el nombre de usuario del usuario.
        return user.UserName;
    }

    public async Task SetNormalizedUserNameAsync(Usuario user, string normalizedName, CancellationToken cancellationToken)
    {
        // Establecer el nombre de usuario normalizado del usuario.
        user.NormalizedUserName = normalizedName;
    }

    public async Task SetUserNameAsync(Usuario user, string userName, CancellationToken cancellationToken)
    {
        // Establecer el nombre de usuario del usuario.
        user.UserName = userName;
    }

    public async Task<IdentityResult> UpdateAsync(Usuario user, CancellationToken cancellationToken)
    {
        // Actualizar el usuario en la base de datos MongoDB.
        var collection = _repository.GetCollection<Usuario>("Usuarios");
        await collection.ReplaceOneAsync(Builders<Usuario>.Filter.Eq(u => u.Id, user.Id), user, cancellationToken);

        return IdentityResult.Success;
    }

    public Task<IdentityResult> DeleteAsync(Usuario user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

*/