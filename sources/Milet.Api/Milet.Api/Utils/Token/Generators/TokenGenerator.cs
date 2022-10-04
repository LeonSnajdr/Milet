using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Samhammer.DependencyInjection.Attributes;

namespace Milet.Api.Utils.Token.Generators;

[Inject(Target.All, ServiceLifetime.Singleton)]
public class TokenGenerator : ITokenGenerator
{
    public string GenerateToken(string secretKey, string issuer, string audience, DateTime utcExpirationTime, IEnumerable<Claim> claims = null)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer,
            audience,
            claims,
            DateTime.UtcNow,
            utcExpirationTime,
            credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

public interface ITokenGenerator
{
    string GenerateToken(string secretKey, string issuer, string audience, DateTime utcExpirationTime, IEnumerable<Claim> claims = null);
}