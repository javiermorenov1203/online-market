using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[ApiController]
[Route("api/[controller]")]
public class AuthController : HomeController
{
    private readonly IConfiguration _config;
    public AuthController(AppDbContext context, IConfiguration config) : base(context)
    {
        _config = config;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(UserDto dto)
    {
        if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
            return BadRequest(new { error = "El email ya está registrado", dto.Email });

        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        var user = new User
        {
            Email = dto.Email,
            Password = hashedPassword
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Usuario registrado correctamente", dto.Email });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserDto dto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
        if (user == null)
            return Unauthorized(new { error = "Email o contraseña inválidos" });

        bool isValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.Password);

        if (!isValid)
            return Unauthorized(new { error = "Email o contraseña inválidos" });

        // 1) Claims del usuario
        var claims = new[]
        {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Email, user.Email)
        };

        // 2) clave secreta que usarás en appsettings.json
        var secret = _config["Jwt:Key"];
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // 3) Crear token
        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: creds);

        string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        // 4) Devolver token al frontend
        return Ok(new
        {
            message = "Login correcto",
            token = tokenString
        });
    }
}

