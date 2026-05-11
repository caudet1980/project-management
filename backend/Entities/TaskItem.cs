namespace TaskManagerApi.Entities;

public class TaskItem
{
    /// <summary>
    /// Unique identifier for the task.
    /// </summary>
    /// <example>3fa85f64-5717-4562-b3fc-2c963f66afa6</example>
    public Guid Id { get; set; } = Guid.NewGuid();
    
    /// <summary>
    /// The title of the task.
    /// </summary>
    public string Title { get; set; } = string.Empty;
    
    /// <summary>
    /// The description of the task.
    /// </summary>
    public string Description { get; set; } = string.Empty;
    
    /// <summary>
    /// Indicates whether the task is completed.
    /// </summary>
    public bool IsCompleted { get; set; } = false;
    
    /// <summary>
    /// Date and time when the task was created.
    /// </summary>
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// The date and time when the task is due.
    /// </summary>
    public DateTime? DueDate { get; set; }
    
    /// <summary>
    /// The unique identifier for the user who owns the task.
    /// </summary>
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
}