namespace ToDoList.API.Repositories.Interfaces;

public interface ITaskRepository
{
    Task<List<TaskItem>> GetAllAsync(int userId);
    Task<List<TaskItem>> GetFilteredAsync(int userId, bool? isCompleted);
    Task<TaskItem?> GetByIdAsync(int id, int userId);
    Task CreateAsync(TaskItem task);
    void Update(TaskItem task);
    void Delete(TaskItem task);
    Task SaveChangesAsync();
}