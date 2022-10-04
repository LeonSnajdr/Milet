using System.ComponentModel.DataAnnotations;

namespace Milet.Api.Contracts;

public class UserLoginContract
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }
}