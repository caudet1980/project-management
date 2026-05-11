public class ErrorLog
{
    /// <summary>
    /// Unique identifier for the error log entry
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The error message that was logged.
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// The inner exception message.
    /// </summary>
    public string InnerMessage { get; set; } = string.Empty;
    /// <summary>
    /// The stack trace of the error.
    /// </summary>
    public string StackTrace { get; set; } = string.Empty;
    
    /// <summary>
    /// The timestamp of when the error occurred.
    /// </summary>
    public DateTime CreatedDate { get; set; }

    /// <summary>
    /// Unique identifier for the user associated with the error.
    /// </summary>
    public Guid? UserId { get; set; }   
}