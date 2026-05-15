public interface ITaskService
{
    Task<IEnumerable<TaskResponseDto>> GetAllAsync(Guid projectId, Guid userId);
    Task<TaskResponseDto?> GetByIdAsync(Guid id, Guid userId);
    Task<TaskResponseDto> CreateAsync(Guid projectId, CreateTaskDto createTaskDto, Guid userId);
    Task<bool> DeleteAsync(Guid id, Guid userId);
}
