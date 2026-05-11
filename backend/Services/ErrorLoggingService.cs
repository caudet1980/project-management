using TaskManagerApi.Data;
using TaskManagerApi.Services.Interface;

namespace TaskManagerApi.Services;
public class ErrorLoggingService : IErrorLoggingService
{
    private readonly TaskManagerDbContext _context;

    public ErrorLoggingService(TaskManagerDbContext context)
        => _context = context;

    public async Task LogErrorAsync(Exception ex)
    {
        var errorLog = new ErrorLog
        {
            Message = ex.Message,
            InnerMessage = ex.InnerException?.Message ?? string.Empty,
            StackTrace = ex?.StackTrace ?? string.Empty,
            CreatedDate = DateTime.UtcNow
        };

        _context.ErrorLogs.Add(errorLog);
        await _context.SaveChangesAsync();
    }
}