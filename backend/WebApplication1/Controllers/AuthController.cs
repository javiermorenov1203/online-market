using Microsoft.AspNetCore.Identity;
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
    public async Task<IActionResult> Register(UserInfo userInfo)
    {
        var user = userInfo.user;
        var userPersonalData = userInfo.userPersonalData;

        if (await _context.Users.AnyAsync(u => u.Email == user.Email))
            return BadRequest(new { error = "El email ya está registrado", user.Email });

        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);

        user.Password = hashedPassword;

        var dbUser = _context.Users.Add(user).Entity;
        await _context.SaveChangesAsync();
        
        userPersonalData.Id = dbUser.Id;
        _context.UserPersonalData.Add(userPersonalData);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Usuario registrado correctamente", user.Email });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(User user)
    {
        var dbUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
        if (dbUser == null)
            return Unauthorized(new { error = "Email o contraseña inválidos" });

        bool isValid = BCrypt.Net.BCrypt.Verify(user.Password, dbUser.Password);

        if (!isValid)
            return Unauthorized(new { error = "Email o contraseña inválidos" });

        // 1) Claims del usuario
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, dbUser.Id.ToString()),
            new Claim(ClaimTypes.Email, dbUser.Email)
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

