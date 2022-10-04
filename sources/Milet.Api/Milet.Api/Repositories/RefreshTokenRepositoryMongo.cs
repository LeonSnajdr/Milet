using Milet.Api.Models;
using MongoDB.Driver;
using Samhammer.DependencyInjection.Attributes;
using Samhammer.Mongo;
using Samhammer.Mongo.Abstractions;

namespace Milet.Api.Repositories;

[Inject]
public class RefreshTokenRepositoryMongo : BaseRepositoryMongo<RefreshTokenModel>, IRefreshTokenRepositoryMongo
{
    public RefreshTokenRepositoryMongo(ILogger<RefreshTokenRepositoryMongo> logger, IMongoDbConnector connector) 
        : base(logger, connector)
    {
    }

    public async Task<RefreshTokenModel> GetByToken(string token)
    {
        var entries = await Collection.FindAsync(refreshTokenModel => refreshTokenModel.Token == token);
        return entries.FirstOrDefault();
    }

    public async Task DeleteByUserId(string userId)
    { 
        await Collection.DeleteManyAsync(refreshTokenModel => refreshTokenModel.UserId == userId);
    }
}

public interface IRefreshTokenRepositoryMongo : IBaseRepositoryMongo<RefreshTokenModel>
{
    Task<RefreshTokenModel> GetByToken(string token);

    Task DeleteByUserId(string userId);
}