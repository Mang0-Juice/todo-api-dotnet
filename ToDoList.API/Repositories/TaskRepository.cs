namespace ToDoList.API.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly AppDbContext _context;

    public TaskRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<TaskItem>> GetAllAsync(int userId)
    {
        return await _context.Tasks
        .Where(t => t.UserId == userId)
        .OrderBy(t => t.DueDate)
        .ToListAsync();
    }

    public async Task<List<TaskItem>> GetFilteredAsync(int userId, bool? isCompleted)
    {
        var query = _context.Tasks.Where(t => t.UserId == userId);

        if (isCompleted.HasValue)
            query.Where(t => t.IsCompleted == isCompleted.Value);
        

        return await query.ToListAsync();
    }

    public async Task<TaskItem?> GetByIdAsync(int id, int userId)
    {
        return await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
    }

    public async Task CreateAsync(TaskItem task)
    {
        await _context.Tasks.AddAsync(task);
        await SaveChangesAsync();
    }

    public void Update(TaskItem task)
    {
        _context.Tasks.Update(task);
    }

    public void Delete(TaskItem task)
    {
        _context.Tasks.Remove(task);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}