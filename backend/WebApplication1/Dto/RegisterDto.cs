
using System.ComponentModel.DataAnnotations;

public class UserDto
{
    public string Email { get; set; }
    [MinLength(6)]
    public string Password { get; set; }
}

