namespace TaskManagerApi.Services.Interface;

public interface IErrorLoggingService
{
    Task LogErrorAsync(Exception ex);
}