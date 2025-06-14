namespace ToDoList.API.Services.Interfaces;

public interface ITaskService
{
    Task<List<TaskReadDTO>> GetAllAsync();
    Task<List<TaskReadDTO>> GetFilteredAsync(bool? isCompleted);
    Task<TaskReadDTO?> GetByIdAsync(int id);
    Task <TaskReadDTO> CreateAsync(TaskCreateDTO dto);
    Task<bool> UpdateAsync(int id, TaskCreateDTO dto);
    Task<bool> DeleteAsync(int id);
}
