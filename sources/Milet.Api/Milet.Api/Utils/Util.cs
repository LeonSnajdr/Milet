using System.Security.Cryptography;
using System.Text;

namespace Milet.Api.Utils;

public static class Util
{
    public static string HashPassword(string password)
    {
        var sha = SHA512.Create();
        var asByteArray = Encoding.Default.GetBytes(password);
        var hashedPassword = sha.ComputeHash(asByteArray) ;
        return Convert.ToBase64String(hashedPassword);
    }
}