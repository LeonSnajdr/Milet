using Milet.Api.Contracts;
using Milet.Api.Models;

namespace Milet.Api.Mapper;

public static class FriendshipMapper
{
    public static FriendshipContract ToContract(this FriendshipModel friendshipModel)
    {
        if (friendshipModel == null) return null;

        return new FriendshipContract
        {
            UserRequestId = friendshipModel.UserRequestId,
            UserAcceptId = friendshipModel.UserAcceptId,
            State = friendshipModel.State,
            DateSent = friendshipModel.DateSent
        };
    }
}