using ToDoList.API.DTOs.Auth;

namespace ToDoList.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _config;

    public AuthController(AppDbContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }

    [HttpPost("Register")]
    public async Task<ActionResult> Register(RegisterDTO dto)
    {
        var existingUser = await _context.Users
        .Where(u => u.Username == dto.Username || u.Email == dto.Email)
        .FirstOrDefaultAsync();

        if (existingUser != null)
        {
            if (existingUser.Username == dto.Username)
            {
                return BadRequest("Username already taken");
            }

            if (existingUser.Email == dto.Email)
            {
                return BadRequest("Email already registered");
            }
        }

        CreatePasswordHash(dto.Password, out byte[] hash, out byte[] salt);

        var user = new User
        {
            Username = dto.Username,
            Email = dto.Email,
            PasswordHash = hash,
            PasswordSalt = salt,

        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok("User registered");

    }

    [HttpPost("Login")]
    public async Task<ActionResult> Login(LoginDTO dto)
    {
        var user = await _context.Users
        .Where(u => u.Username == dto.Login || u.Email == dto.Login)
        .FirstOrDefaultAsync();

        if (user is null || !VerifyPasswordHash(dto.Password, user.PasswordHash!, user.PasswordSalt!))
        {
            return BadRequest("Invalid username or password");
        }

        string token = CreateToken(user);

        return Ok(new { token });
    }

    private void CreatePasswordHash(string password, out byte[] hash, out byte[] salt)
    {
        using var hmac = new HMACSHA512();
        salt = hmac.Key;
        hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    }

    private bool VerifyPasswordHash(string password, byte[] hash, byte[] salt)
    {
        using var hmac = new HMACSHA512(salt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        return computedHash.SequenceEqual(hash);
    }

    private string CreateToken(User user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddHours(2),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}