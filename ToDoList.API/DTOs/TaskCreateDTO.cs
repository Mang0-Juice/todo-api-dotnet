namespace ToDoList.API.DTOs;

public class TaskCreateDTO
{
    [Required(ErrorMessage = "This is required")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "It must be between 3 and 100 characters")]
    public string Title { get; set; } = null!;

    [StringLength(500, ErrorMessage = "Description cannot exced 500 characters")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Due Date is required")]
    public DateTime? DueDate { get; set; }
}
