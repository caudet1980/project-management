using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Data;
using TaskManagerApi.Entities;

public class ProjectService : IProjectService
{
    private readonly TaskManagerDbContext _context;

    public ProjectService(TaskManagerDbContext context)
        => _context = context;

    public async Task<IEnumerable<ProjectResponseDto>> GetAllAsync(Guid userId)
    {
        var projects = await _context.Projects
            .Where(p => p.UserId == userId)
            .Include(p => p.Tasks)
            .ToListAsync();

        return projects.Select(MapToDto);
    }

    public async Task<ProjectResponseDto?> GetByIdAsync(Guid id, Guid userId)
    {
        var project = await _context.Projects
            .Where(p => p.Id == id && p.UserId == userId)
            .Include(p => p.Tasks)
            .FirstOrDefaultAsync();

        return project == null ? null : MapToDto(project);
    }

    public async Task<ProjectResponseDto> CreateAsync(CreateProjectDto createProjectDto, Guid userId)
    {
        var project = new Project
        {
            Title = createProjectDto.Title,
            Description = createProjectDto.Description,
            DueDate = createProjectDto.DueDate,
            UserId = userId
        };

        _context.Projects.Add(project);
        await _context.SaveChangesAsync();

        return MapToDto(project);
    }

    public async Task<bool> DeleteAsync(Guid id, Guid userId)
    {
        var project = await _context.Projects
            .FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId);

        if (project == null) return false;

        _context.Projects.Remove(project);
        await _context.SaveChangesAsync();
        return true;
    }

    private static ProjectResponseDto MapToDto(Project project)
        => new ProjectResponseDto
        {
            Id = project.Id,
            Title = project.Title,
            Description = project.Description,
            DueDate = project.DueDate,
            CreatedDate = project.CreatedDate,
            Tasks = project.Tasks.Select(t => new TaskResponseDto
            {
                Id = t.Id,
                Description = t.Description,
                CreatedDate = t.CreatedDate,
                DueDate = t.DueDate,
                IsCompleted = t.IsCompleted
            }).ToList()
        };
}