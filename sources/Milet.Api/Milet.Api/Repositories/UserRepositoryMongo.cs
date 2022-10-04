using Milet.Api.Models;
using MongoDB.Driver;
using Samhammer.DependencyInjection.Attributes;
using Samhammer.Mongo;
using Samhammer.Mongo.Abstractions;

namespace Milet.Api.Repositories;

[Inject]
public class UserRepositoryMongo : BaseRepositoryMongo<UserModel>, IUserRepositoryMongo
{
    public UserRepositoryMongo(ILogger<UserRepositoryMongo> logger, IMongoDbConnector connector)
        : base(logger, connector)
    {
    }
    
    public async Task<UserModel> GetByUsername(string username)
    {
        var entries = await Collection.FindAsync(userModel => userModel.Username == username);
        return entries.FirstOrDefault();
    }
}

public interface IUserRepositoryMongo : IBaseRepositoryMongo<UserModel>
{
    Task<UserModel> GetByUsername(string username);
}