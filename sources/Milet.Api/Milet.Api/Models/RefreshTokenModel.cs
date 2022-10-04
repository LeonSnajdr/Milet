using Samhammer.Mongo.Abstractions;

namespace Milet.Api.Models;

[MongoCollection]
public class RefreshTokenModel : BaseModelMongo
{
    public string UserId { get; set; }
    
    public string Token { get; set; }
}