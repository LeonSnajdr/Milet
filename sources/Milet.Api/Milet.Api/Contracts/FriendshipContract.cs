using Milet.Api.Utils;

namespace Milet.Api.Contracts;

public class FriendshipContract
{
    public string UserRequestId { get; set; }
    
    public string UserAcceptId { get; set; }
    
    public FriendshipState State { get; set; }
    
    public DateTime DateSent { get; set; }
}