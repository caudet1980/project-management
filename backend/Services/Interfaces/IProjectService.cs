public interface IProjectService
{
    Task<IEnumerable<ProjectResponseDto>> GetAllAsync(Guid userId);
    Task<ProjectResponseDto?> GetByIdAsync(Guid id, Guid userId);
    Task<ProjectResponseDto> CreateAsync(CreateProjectDto createProjectDto, Guid userId);
    Task<bool> DeleteAsync(Guid id, Guid userId);
}