using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Data;
using TaskManagerApi.Entities;

namespace TaskManagerApi.Services;

public class TaskService : ITaskService
{
    private readonly TaskManagerDbContext _context;

    public TaskService(TaskManagerDbContext context)
        => _context = context;

    public async Task<IEnumerable<TaskResponseDto>> GetAllAsync(Guid projectId, Guid userId)
        => await _context.Tasks
            .AsNoTracking()
            .Where(t => t.ProjectId == projectId && t.UserId == userId)
            .Select(t => MapToDto(t)).ToListAsync();

    public async Task<TaskResponseDto?> GetByIdAsync(Guid taskId, Guid userId)
        => await _context.Tasks
            .AsNoTracking()
            .Where(t => t.Id == taskId && t.UserId == userId)
            .Select(t => MapToDto(t)).FirstOrDefaultAsync();

    public async Task<TaskResponseDto> CreateAsync(Guid projectId, CreateTaskDto createTaskDto, Guid userId)
    {
        var projectExists = await _context.Projects
            .AnyAsync(p => p.Id == projectId && p.UserId == userId);

        if(!projectExists)
            throw new UnauthorizedAccessException("Project not found.");

        var task = new TaskItem
        {
            ProjectId = projectId,
            Description = createTaskDto.Description,
            IsCompleted = false,
            CreatedDate = DateTime.UtcNow,
            DueDate = createTaskDto?.DueDate,
            UserId = userId
        };
        await _context.Tasks.AddAsync(task);
        await _context.SaveChangesAsync();

        return MapToDto(task);
    }

    public async Task<bool> DeleteAsync(Guid taskId, Guid userId)
    {
        var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == taskId && t.UserId == userId);
        if (task == null) 
            return false;

        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();
        return true;
    }

    #region Private Methods
    private static TaskResponseDto MapToDto(TaskItem task)
        => new TaskResponseDto
        {
            Id = task.Id,
            Description = task.Description,
            IsCompleted = task.IsCompleted,
            CreatedDate = task.CreatedDate,
            DueDate = task.DueDate
        };
    #endregion
}