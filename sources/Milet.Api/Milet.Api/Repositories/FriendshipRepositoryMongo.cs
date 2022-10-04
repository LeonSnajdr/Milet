using Milet.Api.Models;
using Milet.Api.Utils;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Samhammer.DependencyInjection.Attributes;
using Samhammer.Mongo;
using Samhammer.Mongo.Abstractions;

namespace Milet.Api.Repositories;

[Inject]
public class FriendshipRepositoryMongo: BaseRepositoryMongo<FriendshipModel>, IFriendshipRepositoryMongo
{
    public FriendshipRepositoryMongo(ILogger<FriendshipRepositoryMongo> logger, IMongoDbConnector connector) 
        : base(logger, connector)
    {
    }

    public async Task<FriendshipModel> GetFriendship(string userRequestId, string userAcceptId)
    {
        var entries = await Collection.FindAsync(friendship =>
            friendship.UserRequestId == userRequestId && friendship.UserAcceptId == userAcceptId);
        return entries.FirstOrDefault();
    }

    public async Task<bool> FriendshipExists(string userRequestId, string userAcceptId)
    {
        var result = await Collection.AsQueryable().AnyAsync(friendship => friendship.UserRequestId == userRequestId && friendship.UserAcceptId == userAcceptId);
        return result;
    }

    public async Task<List<FriendshipModel>> GetFriendships(string userId)
    {
        var entries = await Collection.FindAsync(friendship =>
            friendship.UserRequestId == userId || friendship.UserAcceptId == userId);
        
        return entries.ToList();
    }

    public async Task UpdateFriendshipState(string friendshipId, FriendshipState state)
    {
        var update = Builders<FriendshipModel>.Update
            .Set(nameof(FriendshipModel.State), state);
        
        var filters = new List<FilterDefinition<FriendshipModel>>
        {
            Builders<FriendshipModel>.Filter.Where(x => x.Id == friendshipId)
        };

        await Collection.UpdateOneAsync(Builders<FriendshipModel>.Filter.And(filters), update);
    }
}

public interface IFriendshipRepositoryMongo : IBaseRepositoryMongo<FriendshipModel>
{
    
    Task<FriendshipModel> GetFriendship(string userRequestId, string userAcceptId);

    Task<bool> FriendshipExists(string userRequestId, string userAcceptId);

    Task<List<FriendshipModel>> GetFriendships(string userId);

    Task UpdateFriendshipState(string friendshipId, FriendshipState state);
}