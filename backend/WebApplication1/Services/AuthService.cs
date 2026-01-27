
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class AuthService
{
    private readonly IConfiguration _config;
    private readonly AppDbContext _context;

    public AuthService(IConfiguration config, AppDbContext context)
    {
        _config = config;
        _context = context;
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<UserInfo> RegisterUserAsync(UserInfoDto dto)
    {
        User user = new User(dto.userDto);
        UserPersonalData userPersonalData = new UserPersonalData(dto.userPersonalDataDto);

        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);
        user.Password = hashedPassword;

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        userPersonalData.UserId = user.Id;
        _context.UserPersonalData.Add(userPersonalData);
        await _context.SaveChangesAsync();

        return new UserInfo
        {
            user = user,
            userPersonalData = userPersonalData
        };
    }

    public async Task<bool> ValidatePasswordAsync(string password, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }

    public string GenerateJwtToken(User user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email)
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_config["Jwt:Key"])
        );

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            expires: DateTime.UtcNow.AddHours(2),
            claims: claims,
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
