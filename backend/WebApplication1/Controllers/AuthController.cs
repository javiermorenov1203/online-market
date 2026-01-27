using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(UserInfoDto userInfoDto)
    {
        if (await _authService.GetUserByEmailAsync(userInfoDto.userDto.Email) != null)
            return BadRequest(new { error = "Email has already been registered", userInfoDto.userDto.Email });

        var userInfo = await _authService.RegisterUserAsync(userInfoDto);

        return Ok(new
        {
            message = "User registered successfully",
            userInfo
        });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserDto userDto)
    {
        User dbUser = await _authService.GetUserByEmailAsync(userDto.Email);

        // User is not registered
        if (dbUser == null)
            return Unauthorized(new { error = "Email or password are invalid" });

        bool isPasswordValid = await _authService.ValidatePasswordAsync(userDto.Password, dbUser.Password);

        if (!isPasswordValid)
            return Unauthorized(new { error = "Email or password are invalid" });

        string tokenString = _authService.GenerateJwtToken(dbUser);
        
        return Ok(new
        {
            message = "Successful login",
            token = tokenString
        });
    }

    [HttpGet("check-email-exists")]
    public async Task<IActionResult> CheckIfEmailExists([FromQuery] string email)
    {
        if (await _authService.GetUserByEmailAsync(email) == null)
            return NotFound(new { message = "Email not found" });

        return Ok(new { message = "Email found" });
    }
}

