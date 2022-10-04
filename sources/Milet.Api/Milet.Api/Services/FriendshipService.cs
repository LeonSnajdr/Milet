using Milet.Api.Contracts;
using Milet.Api.Models;
using Milet.Api.Repositories;
using Milet.Api.Utils;
using Milet.Api.Mapper;
using Samhammer.DependencyInjection.Attributes;

namespace Milet.Api.Services;

[Inject]
public class FriendshipService : IFriendshipService
{
    private IFriendshipRepositoryMongo Repository { get; }

    public FriendshipService(IFriendshipRepositoryMongo repository)
    {
        Repository = repository;
    }

    public async Task<bool> Exists(string userRequestId, string userAcceptId)
    {
        return await Repository.FriendshipExists(userRequestId, userAcceptId);
    }

    public async Task<FriendshipContract> RequestFriendship(string userRequestId, string userAcceptId)
    {
        var friendShip = new FriendshipModel
        {
            UserRequestId = userRequestId,
            UserAcceptId = userAcceptId,
            State = FriendshipState.Waiting,
            DateSent = DateTime.UtcNow
        };

        await Repository.Save(friendShip);

        return friendShip.ToContract();
    }

    public async Task AcceptFriendship(string friendshipId)
    {
        await Repository.UpdateFriendshipState(friendshipId, FriendshipState.Accepted);
    }
}

public interface IFriendshipService
{
    Task<bool> Exists(string userRequestId, string userAcceptId);

    Task<FriendshipContract> RequestFriendship(string userRequestId, string userAcceptId);

    Task AcceptFriendship(string friendshipId);
}