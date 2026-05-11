using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Data;
using TaskManagerApi.DTOs.Auth;
using TaskManagerApi.Entities;
using TaskManagerApi.Services.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace TaskManagerApi.Services;

public class AuthService : IAuthService
{
    private readonly TaskManagerDbContext _context;
    private readonly IConfiguration _configuration;
    public AuthService(TaskManagerDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto)
    {
        var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == registerDto.Email);
        if(existingUser != null)
            throw new InvalidOperationException("User with this email already exists.");

            var user = new User
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password)
            };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            var token = GenerateJwtToken(user);
            
            return new AuthResponseDto
            {
                Token = token,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };
    }

    public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);
        if(user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
            throw new UnauthorizedAccessException("Invalid email or password.");

        var token = GenerateJwtToken(user);
        return new AuthResponseDto
        {
            Token = token,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email
        };
    }

    #region Private Methods
    private string GenerateJwtToken(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var credentionals = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new []
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(7),
            signingCredentials: credentionals
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    #endregion
}