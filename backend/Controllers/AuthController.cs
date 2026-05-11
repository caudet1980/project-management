using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.Services.Interface;
using TaskManagerApi.DTOs;
using TaskManagerApi.DTOs.Auth;

[ApiController]
[Route("api/[controller]")]
public class AuthController : BaseApiController
{
    private readonly IAuthService _authService;
    
    public AuthController(IAuthService authService, IErrorLoggingService errorLoggingService)
        : base(errorLoggingService)
        => _authService = authService;
    
    #region POST
    /// <summary>
    /// Authenticates a user and generates a JWT token.
    /// </summary>
    /// <param name="loginDto">The user's login information.</param>
    /// <returns>An object containing the JWT token and the user's information.</returns>
    /// <response code="200">The user has been authenticated successfully.</response>
    /// <response code="400">The login data is invalid.</response>
    /// <response code="401">Incorrect email or password.</response>
    /// <response code="500">An internal error occurred.</response>    [HttpPost]
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var result = await _authService.LoginAsync(loginDto);
            return Ok(result);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
        catch (InvalidCastException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return await HandleException(ex);
        }
    }


    /// <summary>
    /// Registers a new user.
    /// </summary>
    /// <param name="registerDto">The user's registration information.</param>
    /// <returns>An object containing the JWT token and the user's information.</returns>
    /// <response code="200">The user has been registered successfully.</response>
    /// <response code="400">A user with this email already exists or the data is invalid.</response>
    /// <response code="500">An internal error occurred.</response>
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        try
        {
            var result = await _authService.RegisterAsync(registerDto);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return await HandleException(ex);
        }
    }
    #endregion
    
}