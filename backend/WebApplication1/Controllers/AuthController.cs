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
            return BadRequest(new { error = "Email has already been registered", user.Email });

        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);

        user.Password = hashedPassword;

        var dbUser = _context.Users.Add(user).Entity;
        await _context.SaveChangesAsync();

        userPersonalData.UserId = dbUser.Id;
        await _context.UserPersonalData.AddAsync(userPersonalData);
        await _context.SaveChangesAsync();

        return Ok(new { message = "User registered successfully", user.Email });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(User user)
    {
        var dbUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
        if (dbUser == null)
            return Unauthorized(new { error = "Email or password are invalid" });

        bool isValid = BCrypt.Net.BCrypt.Verify(user.Password, dbUser.Password);

        if (!isValid)
            return Unauthorized(new { error = "Email or password are invalid" });

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
            message = "Successful login",
            token = tokenString
        });
    }

    [HttpGet("check-email-exists")]
    public async Task<IActionResult> CheckEmailExists([FromQuery] string email)
    {
        bool exists = await _context.Users.AnyAsync(u => u.Email == email);

        if (!exists)
            return NotFound(new { message = "User not found" });
        
        return Ok(new { message = "User found" });
    }
}

