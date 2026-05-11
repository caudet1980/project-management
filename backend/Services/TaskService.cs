using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Data;
using TaskManagerApi.Entities;

namespace TaskManagerApi.Services;

public class TaskService : ITaskService
{
    private readonly TaskManagerDbContext _context;

    public TaskService(TaskManagerDbContext context)
        => _context = context;

    public async Task<IEnumerable<TaskResponseDto>> GetAllAsync(Guid userId)
        => await _context.Tasks
            .AsNoTracking()
            .Where(t => t.UserId == userId)
            .Select(t => MapToDto(t)).ToListAsync();

    public async Task<TaskResponseDto?> GetByIdAsync(Guid id, Guid userId)
        => await _context.Tasks
            .AsNoTracking()
            .Where(t => t.Id == id && t.UserId == userId)
            .Select(t => MapToDto(t)).FirstOrDefaultAsync();

    public async Task<TaskResponseDto> CreateAsync(CreateTaskDto createTaskDto, Guid userId)
    {
        var task = new Entities.TaskItem
        {
            Title = createTaskDto.Title,
            Description = createTaskDto?.Description ?? string.Empty,
            IsCompleted = false,
            CreatedDate = DateTime.UtcNow,
            DueDate = createTaskDto?.DueDate,
            UserId = userId
        };
        await _context.Tasks.AddAsync(task);
        await _context.SaveChangesAsync();

        return MapToDto(task);
    }

    public async Task<TaskResponseDto?> UpdateAsync(Guid id, UpdateTaskDto updateTaskDto, Guid userId)
    {
        var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
        if (task == null) 
            return null;

        task.Title = updateTaskDto.Title ?? task.Title;
        task.Description = updateTaskDto.Description ?? task.Description;
        task.IsCompleted = updateTaskDto?.IsCompleted ?? task.IsCompleted;
        task.DueDate = updateTaskDto?.DueDate ?? task.DueDate;

        await _context.SaveChangesAsync();

        return MapToDto(task);
    }

    public async Task<bool> DeleteAsync(Guid id, Guid userId)
    {
        var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
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
            Title = task.Title,
            Description = task.Description,
            IsCompleted = task.IsCompleted,
            CreatedDate = task.CreatedDate,
            DueDate = task.DueDate
        };
    #endregion
}