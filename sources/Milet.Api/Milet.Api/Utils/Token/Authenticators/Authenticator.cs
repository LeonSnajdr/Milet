using Milet.Api.Contracts;
using Milet.Api.Models;
using Milet.Api.Repositories;
using Milet.Api.Utils.Token.Generators;
using Milet.Api.Utils.Token.Validators;
using Samhammer.DependencyInjection.Attributes;
using Serilog;

namespace Milet.Api.Utils.Token.Authenticators;

[Inject]
public class Authenticator : IAuthenticator
{
    private IRefreshTokenRepositoryMongo RefreshTokenRepository { get;  }
    
    private IAccessTokenGenerator AccessTokenGenerator { get;  }
    
    private IRefreshTokenGenerator RefreshTokenGenerator { get; }

    public Authenticator(IRefreshTokenRepositoryMongo refreshTokenRepository, IAccessTokenGenerator accessTokenGenerator, IRefreshTokenGenerator refreshTokenGenerator)
    {
        RefreshTokenRepository = refreshTokenRepository;
        AccessTokenGenerator = accessTokenGenerator;
        RefreshTokenGenerator = refreshTokenGenerator;
    }

    public async Task<TokenContract> Authenticate(UserContract userContract)
    {
        var accessToken = AccessTokenGenerator.GenerateToken(userContract);

        var refreshToken = RefreshTokenGenerator.GenerateToken();
        
        var refreshTokenContract = new RefreshTokenModel
        {
            Token = refreshToken,
            UserId = userContract.Id
        };

        await RefreshTokenRepository.Save(refreshTokenContract);

        return new TokenContract
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }
}

public interface IAuthenticator
{
    Task<TokenContract> Authenticate(UserContract userContract);
}