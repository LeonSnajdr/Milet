using Microsoft.Extensions.Options;
using Milet.Api.Options;
using Samhammer.DependencyInjection.Attributes;

namespace Milet.Api.Utils.Token.Generators;

[Inject(Target.All, ServiceLifetime.Singleton)]
public class RefreshTokenGenerator : IRefreshTokenGenerator
{
    private IOptions<JwtOptions> JwtOptions { get; }
    private ITokenGenerator TokenGenerator { get; }

    public RefreshTokenGenerator(IOptions<JwtOptions> jwtOptions, ITokenGenerator tokenGenerator)
    {
        JwtOptions = jwtOptions;
        TokenGenerator = tokenGenerator;
    }

    public string GenerateToken()
    {
        var expirationTime = DateTime.UtcNow.AddMinutes(JwtOptions.Value.RefreshTokenExpirationMinutes);

        return TokenGenerator.GenerateToken(
            JwtOptions.Value.RefreshTokenSecret,
            JwtOptions.Value.Issuer,
            JwtOptions.Value.Audience,
            expirationTime);
    }
}

public interface IRefreshTokenGenerator
{
    string GenerateToken();
}