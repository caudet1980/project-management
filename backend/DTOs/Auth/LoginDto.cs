namespace TaskManagerApi.DTOs.Auth;

public class LoginDto
{
    /// <summary>
    /// User's email address.
    /// </summary>
    /// <example>test@test.com</example>
    public string Email { get; set; } = string.Empty;
    
    /// <summary>
    /// User's password.
    /// </summary>
    /// <example>Test1234!</example>
    public string Password { get; set; } = string.Empty;
}