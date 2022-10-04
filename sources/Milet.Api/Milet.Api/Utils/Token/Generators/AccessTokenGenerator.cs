using System.Security.Claims;
using Microsoft.Extensions.Options;
using Milet.Api.Contracts;
using Milet.Api.Options;
using Samhammer.DependencyInjection.Attributes;

namespace Milet.Api.Utils.Token.Generators;

[Inject(Target.All, ServiceLifetime.Singleton)]
public class AccessTokenGenerator : IAccessTokenGenerator
{
    private IOptions<JwtOptions> JwtOptions { get; }
    private ITokenGenerator TokenGenerator { get; }

    public AccessTokenGenerator(IOptions<JwtOptions> jwtOptions, ITokenGenerator tokenGenerator)
    {
        JwtOptions = jwtOptions;
        TokenGenerator = tokenGenerator;
    }

    public string GenerateToken(UserContract userContract)
    {
        var claims = new List<Claim>
        {
            new("Id", userContract.Id),
            new(ClaimTypes.Name, userContract.Username),
            new(ClaimTypes.Email, userContract.Email),
            new(ClaimTypes.Role, userContract.Role),
        };
        
        var expirationTime = DateTime.UtcNow.AddMinutes(JwtOptions.Value.AccessTokenExpirationMinutes);

        return TokenGenerator.GenerateToken(
            JwtOptions.Value.AccessTokenSecret,
            JwtOptions.Value.Issuer,
            JwtOptions.Value.Audience,
            expirationTime,
            claims);
    }
}

public interface IAccessTokenGenerator
{
    string GenerateToken(UserContract userContract);
}