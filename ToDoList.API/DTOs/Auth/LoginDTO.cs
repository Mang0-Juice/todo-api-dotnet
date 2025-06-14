namespace ToDoList.API.DTOs.Auth;

public class LoginDTO : IValidatableObject
{
    [Required]
    public string Login { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;

    public IEnumerable<ValidationResult> Validate(ValidationContext context)
    {
        if (string.IsNullOrEmpty(Login))
        {
            yield return new ValidationResult("Email address or username required", [nameof(Login)] );
        }
    }
}