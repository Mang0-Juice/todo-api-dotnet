namespace ToDoList.API.DTOs;

public class TaskReadDTO
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public bool IsCompleted { get; set; } = false;
    public DateTime CreatedDate { get; set; }
    public DateTime? DueDate { get; set; }
}
