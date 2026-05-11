public interface ITaskService
{
    Task<IEnumerable<TaskResponseDto>> GetAllAsync(Guid userId);
    Task<TaskResponseDto?> GetByIdAsync(Guid id, Guid userId);
    Task<TaskResponseDto> CreateAsync(CreateTaskDto createTaskDto, Guid userId);
    Task<TaskResponseDto?> UpdateAsync(Guid id, UpdateTaskDto updateTaskDto, Guid userId);
    Task<bool> DeleteAsync(Guid id, Guid userId);
}