using AutoMapper;

namespace ToDoList.API.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _repository;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContext;

    public TaskService(ITaskRepository repository, IMapper mapper, IHttpContextAccessor httpContext)
    {
        _repository = repository;
        _mapper = mapper;
        _httpContext = httpContext;
    }

    public async Task<List<TaskReadDTO>> GetAllAsync()
    {
        var userId = GetUserId();
        var tasks = await _repository.GetAllAsync(userId);
        return _mapper.Map<List<TaskReadDTO>>(tasks);
    }

    public async Task<TaskReadDTO?> GetByIdAsync(int id)
    {
        var userId = GetUserId();
        var task = await _repository.GetByIdAsync(id, userId);

        if (task == null || task.UserId != userId)
            return null;

        return _mapper.Map<TaskReadDTO>(task);
    }

    public async Task<List<TaskReadDTO>> GetFilteredAsync(bool? isCompleted)
    {
        var userId = GetUserId();
        var task = await _repository.GetFilteredAsync(userId, isCompleted);
        return _mapper.Map<List<TaskReadDTO>>(task);
    }

    public async Task<TaskReadDTO> CreateAsync(TaskCreateDTO dto)
    {
        var userId = GetUserId();

        var task = _mapper.Map<TaskItem>(dto);
        task.UserId = userId;

        await _repository.CreateAsync(task);
        return _mapper.Map<TaskReadDTO>(task);
    }

    public async Task<bool> UpdateAsync(int id, TaskCreateDTO dto)
    {
        var userId = GetUserId();

        var task = await _repository.GetByIdAsync(id, userId);

        if (!IsTaskOwnedByUser(task, userId))
            return false;

        _mapper.Map(dto, task);

        _repository.Update(task!);

        await _repository.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var userId = GetUserId();

        var task = await _repository.GetByIdAsync(id, userId);

        if (!IsTaskOwnedByUser(task, userId))
            return false;

        _repository.Delete(task!);

        await _repository.SaveChangesAsync();

        return true;
    }

    private int GetUserId()
    {
        var userIdClaim = (_httpContext.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier));

        if (userIdClaim == null)
        {
            throw new UnauthorizedAccessException("User id not found in token");
        }

        return int.Parse(userIdClaim.Value);
    }

    public bool IsTaskOwnedByUser(TaskItem? task, int userId) => task == null || task.UserId == userId;

}
