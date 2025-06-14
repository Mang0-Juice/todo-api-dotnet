using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ToDoList.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TaskController : ControllerBase
{
    private readonly ITaskService _service;
    public TaskController(ITaskService service)
    {
        _service = service;
    }

    /// <summary>List all tasks</summary>
    [HttpGet]
    public async Task<ActionResult<List<TaskReadDTO>>> GetAll()
    {
        var tasks = await _service.GetAllAsync();
        return tasks == null ? NotFound("No tasks have been found to list") : Ok(tasks);
    }

    /// <summary>Returns a task by id</summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<TaskReadDTO>> GetById(int id)
    {
        var task = await _service.GetByIdAsync(id);
        if (task == null)
            return NotFound($"Task with ID {id} not found");

        return Ok(task);
    }

    /// <summary>Returns a filtered list of tasks </summary>
    [HttpGet("filter")]
    public async Task<ActionResult<List<TaskReadDTO>>> GetFiltered([FromBody] bool? isCompleted)
    {
        var tasks = await _service.GetFilteredAsync(isCompleted);
        return Ok(tasks);
    }

    /// <summary>Creates a new task</summary>
    [HttpPost]
    public async Task<ActionResult<TaskReadDTO>> Create([FromBody] TaskCreateDTO dto)
    {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    /// <summary>Updates an existing task</summary>
    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] TaskCreateDTO dto)
    {
        var updated = await _service.UpdateAsync(id, dto);
        return !updated ? NotFound($"Task with ID {id} not found to update") : NoContent();
    }

    /// <summary>Deletes a task</summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        return !deleted ? NotFound($"Task with ID {id} not found to delete") : NoContent();
        
    }
}