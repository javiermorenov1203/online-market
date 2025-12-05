using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;

[ApiController]
[Route("api/[controller]")]
public class AuthController : HomeController
{
    public AuthController(AppDbContext context) : base(context)
    {
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
            return BadRequest(new { error = "El email ya está registrado", dto.Email});

        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        var user = new User
        {
            Email = dto.Email,
            Password = hashedPassword
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Usuario registrado correctamente", dto.Email});
    }

}

