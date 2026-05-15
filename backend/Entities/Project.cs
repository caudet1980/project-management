namespace TaskManagerApi.Entities;

public class Project
{
    /// <summary>
    /// Unique identifier for the project
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Project title
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Project description
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Project due date
    /// </summary>
    public DateTime? DueDate { get; set; }
    
    /// <summary>
    ///  Date and time when the project was created.
    /// </summary>
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// User unique identifier
    /// </summary>
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    /// <summary>
    /// List of project tasks
    /// </summary>
    public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
}