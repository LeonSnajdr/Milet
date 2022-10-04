using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Milet.Api.Contracts;
using Milet.Api.Repositories;
using Milet.Api.Utils;
using Milet.Api.Utils.Token.Authenticators;
using Milet.Api.Utils.Token.Validators;
using Milet.Api.Mapper;
using Milet.Api.Models;
using Milet.Api.Options;
using Milet.Api.Utils.Token.Generators;
using Samhammer.DependencyInjection.Attributes;

namespace Milet.Api.Services;

[Inject]
public class AuthService : IAuthService
{
    private IUserRepositoryMongo UserRepository{ get; }
    
    private IRefreshTokenRepositoryMongo RefreshTokenRepository { get;  }
    
    private IRefreshTokenValidator RefreshTokenValidator { get; }
    
    private IAuthenticator Authenticator { get;  }
    
    private ILogger<AuthService> Logger { get; }

    public AuthService(IUserRepositoryMongo userRepository, ILogger<AuthService> logger, IRefreshTokenValidator refreshTokenValidator, IRefreshTokenRepositoryMongo refreshTokenRepository, IAuthenticator authenticator)
    {
        UserRepository = userRepository;
        Logger = logger;
        RefreshTokenValidator = refreshTokenValidator;
        RefreshTokenRepository = refreshTokenRepository;
        Authenticator = authenticator;
    }
    
    public async Task<AuthResponseContract> Login(UserLoginContract userLogin)
    {
        var userModel = await UserRepository.GetByUsername(userLogin.Username);

        if (userModel == null || userModel.Password != Util.HashPassword(userLogin.Password)) return null;
        
        var user = userModel.ToContract();

        var token = await Authenticator.Authenticate(user);
        
        return new AuthResponseContract
        {
            Token = token,
            User = user
        };
    }

    public async Task Logout(string userId)
    {
        await RefreshTokenRepository.DeleteByUserId(userId);
    }

    public async Task<TokenContract> Refresh(TokenContract oldToken)
    {
        var isValidToken = RefreshTokenValidator.Validate(oldToken.RefreshToken);

        if (!isValidToken) return null;

        var refreshToken = await RefreshTokenRepository.GetByToken(oldToken.RefreshToken);

        if (refreshToken == null) return null;

        await RefreshTokenRepository.Delete(refreshToken);

        var userModel = await UserRepository.GetById(refreshToken.UserId);

        if (userModel == null) return null;
        
        var user = userModel.ToContract();

        var token = await Authenticator.Authenticate(user);

        return token;

    }
}

public interface IAuthService
{
    Task<AuthResponseContract> Login(UserLoginContract userLogin);

    Task Logout(string userId);

    Task<TokenContract> Refresh(TokenContract token);
}