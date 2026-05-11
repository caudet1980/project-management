using Moq;
using TaskManagerApi.Services;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;
using TaskManagerApi.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using TaskManagerApi.DTOs.Auth;
using TaskManagerApi.Entities;

public class AuthServiceTests: BaseTests
{
    private readonly TaskManagerDbContext _context;
    private readonly Mock<IConfiguration> _configuration = new();
    private readonly AuthService _authService;
    private readonly User _user = new User
    {
        FirstName = "Jane",
        LastName = "Doe",
        Email = "test@test.com",
        PasswordHash = BCrypt.Net.BCrypt.HashPassword("password123!")
    };

    public AuthServiceTests()
    {
        _configuration.Setup(c => c["Jwt:Key"]).Returns("supersecretkeywithatlesat128bits");
        _configuration.Setup(c => c["Jwt:Issuer"]).Returns("TaskManagerApi");
        _configuration.Setup(c => c["Jwt:Audience"]).Returns("TaskManagerUsers");

        _context = new TaskManagerDbContext(DbOptions);
        _authService = new AuthService(_context, _configuration.Object);
    }

    #region LoginAsync
    [Theory]
    [MemberData(nameof(GetLoginTestData))]
    public async Task LoginAsync_Should_Return_Correct_Result(string email, string password, bool isSuccess)
    {
        // Arrange
        _context.Users.Add(_user);
        await _context.SaveChangesAsync();
        var loginDto = new LoginDto
        {
            Email = email,
            Password = password
        };

        // Act
        if (isSuccess)        { 
            var result = await _authService.LoginAsync(loginDto ); 
            
            // Assert
            Assert.NotNull(result);
            Assert.Equal(email, result.Email);
            Assert.Equal("Jane", result.FirstName);
            Assert.Equal("Doe", result.LastName);
            Assert.False(string.IsNullOrEmpty(result.Token));
        }
        else
        {
            await Assert.ThrowsAsync<UnauthorizedAccessException>(() => _authService.LoginAsync(loginDto));
        }
    }
    #endregion

    #region MemberData
    public static IEnumerable<object[]> GetLoginTestData()
    {
        // Valid credentials
        yield return new object[] { "test@test.com", "password123!", true };
        // Invalid password
        yield return new object[] { "test@test.com", "wrongpassword", false };
        // Invalid email
        yield return new object[] { "wrongemail@test.com", "password123!", false };
        // Non-existent user
        yield return new object[] { "nonexistentuser@test.com", "wrongpassword", false };
    }
    #endregion
}

    