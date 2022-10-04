using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Milet.Api.Options;
using Samhammer.DependencyInjection.Attributes;

namespace Milet.Api.Utils.Token.Validators;

[Inject(Target.All, ServiceLifetime.Singleton)]
public class RefreshTokenValidator : IRefreshTokenValidator
{
    private IOptions<JwtOptions> JwtOptions { get; }

    public RefreshTokenValidator(IOptions<JwtOptions> jwtOptions)
    {
        JwtOptions = jwtOptions;
    }

    public bool Validate(string refreshToken)
    {
        var validationParameters = new TokenValidationParameters()
        {
            ValidateActor = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = JwtOptions.Value.Issuer,
            ValidAudience = JwtOptions.Value.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtOptions.Value.RefreshTokenSecret)),
            ClockSkew = TimeSpan.Zero
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        try
        {
            tokenHandler.ValidateToken(refreshToken, validationParameters, out var validatedToken);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}

public interface IRefreshTokenValidator
{
    bool Validate(string refreshToken);
}