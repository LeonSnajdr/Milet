using Samhammer.Mongo.Abstractions;

namespace Milet.Api.Models;

[MongoCollection]
public class UserModel : BaseModelMongo
{
    public string Username { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public string GivenName { get; set; }

    public string Surname { get; set; }

    public string Role { get; set; }


}