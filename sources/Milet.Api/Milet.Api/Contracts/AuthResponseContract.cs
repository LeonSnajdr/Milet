namespace Milet.Api.Contracts;

public class AuthResponseContract
{
    public TokenContract Token { get; set; }
    
    public UserContract User { get; set; }
}