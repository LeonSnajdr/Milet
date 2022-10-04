using Milet.Api.Contracts;
using Milet.Api.Repositories;
using Milet.Api.Utils;
using Milet.Api.Mapper;
using Milet.Api.Models;
using Samhammer.DependencyInjection.Attributes;

namespace Milet.Api.Services;

[Inject]
public class UserService : IUserService
{
    private IUserRepositoryMongo Repository { get; }

    public UserService(IUserRepositoryMongo repository)
    {
        Repository = repository;
    }

    public async Task<UserContract> Create(CreateUserContract createUser)
    {
        var userModel = createUser.ToModel();
        userModel.Password = Util.HashPassword(userModel.Password);
        await Repository.Save(userModel);

        return userModel.ToContract();
    }

    public async Task<UserContract> GetById(string userId)
    {
        var userModal = await Repository.GetById(userId);

        return userModal.ToContract();
    }

    public async Task<bool> Exists(string username)
    {
        var userModel = await Repository.GetByUsername(username);

        return userModel != null;
    }
    
}

public interface IUserService
{
    Task<UserContract> Create(CreateUserContract createUser);

    Task<UserContract> GetById(string userId);

    Task<bool> Exists(string username);
    
}