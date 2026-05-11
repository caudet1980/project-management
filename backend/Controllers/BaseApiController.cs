using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.Services.Interface;

public class BaseApiController : ControllerBase
{
    protected Guid UserId => GetUserId();
    protected readonly IErrorLoggingService _errorLoggingService;

    public BaseApiController(IErrorLoggingService errorLoggingService)
        => _errorLoggingService = errorLoggingService;

    protected Guid GetUserId()
        => Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId)
            ? userId
            : throw new UnauthorizedAccessException("User ID claim is missing or invalid.");

    protected async Task<IActionResult> HandleException(Exception ex)
    {
        // Logger l'erreur
        await _errorLoggingService.LogErrorAsync(ex);
        return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
    }
}