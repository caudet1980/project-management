public class ProjectResponseDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime? DueDate { get; set; }
    public DateTime CreatedDate { get; set; }
    public List<TaskResponseDto> Tasks { get; set; } = new List<TaskResponseDto>();
}