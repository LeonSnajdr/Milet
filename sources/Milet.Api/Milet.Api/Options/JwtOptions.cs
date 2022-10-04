using Samhammer.Options.Abstractions;

namespace Milet.Api.Options;

[Option]
public class JwtOptions
{
    public string AccessTokenSecret { get; set; }
    
    public string RefreshTokenSecret { get; set; }
    
    public double AccessTokenExpirationMinutes { get; set; }
    
    public double RefreshTokenExpirationMinutes { get; set; }
    
    public string Issuer { get; set; }
    
    public string Audience { get; set; }
}