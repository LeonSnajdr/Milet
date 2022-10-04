using Milet.Api.Contracts;
using Milet.Api.Models;

namespace Milet.Api.Mapper;

public static class UserMapper
{
    public static UserContract ToContract(this UserModel userModel)
    {
        if (userModel == null) return null;

        return new UserContract
        {
            Id = userModel.Id,
            Username = userModel.Username,
            Email = userModel.Email,
            GivenName = userModel.GivenName,
            Surname = userModel.Surname,
            Role = userModel.Role
        };
    }

    public static UserModel ToModel(this CreateUserContract createUserContract)
    {
        if (createUserContract == null) return null;

        return new UserModel
        {
            Username = createUserContract.Username,
            Email = createUserContract.Email,
            Password = createUserContract.Password,
            GivenName = createUserContract.GivenName,
            Surname = createUserContract.Surname,
            Role = createUserContract.Role
        };
    }
}