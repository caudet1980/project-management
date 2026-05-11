using TaskManagerApi.DTOs.Auth;

namespace TaskManagerApi.Services.Interface;

public interface IAuthService
{   
    Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto);
    Task<AuthResponseDto> LoginAsync(LoginDto loginDto);
}