using Milet.Api.Utils;
using Samhammer.Mongo.Abstractions;

namespace Milet.Api.Models;

public class FriendshipModel : BaseModelMongo
{
    public string UserRequestId { get; set; }
    
    public string UserAcceptId { get; set; }
    
    public FriendshipState State { get; set; }
    
    public DateTime DateSent { get; set; }
}