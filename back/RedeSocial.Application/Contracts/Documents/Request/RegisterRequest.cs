using System.ComponentModel.DataAnnotations;

namespace RedeSocial.Application.Contracts.Documents.Request;

public class RegisterRequest
{
    [Required (ErrorMessage = "Name is required"), 
    MinLength(2, ErrorMessage = "Name must be at least 2 characters long"), 
    MaxLength(250, ErrorMessage = "Name must be at most 250 characters long")]
    public string Name { get; set; } = string.Empty;

    [Required (ErrorMessage = "Email is required"), 
    EmailAddress (ErrorMessage = "Invalid email format"), 
    MaxLength(250, ErrorMessage = "Email must be at most 250 characters long")]
    public string Email { get; set; } = string.Empty;

    [Required (ErrorMessage = "Password is required"),
    MaxLength(128, ErrorMessage = "Password must be at most 128 characters long")]
    public string Password { get; set; } = string.Empty;

    [Required (ErrorMessage = "BirthDate is required"), 
    DataType(DataType.Date)]
    public DateOnly BirthDate { get; set; }

    [Required (ErrorMessage = "Cep is required"), 
    MaxLength(8, ErrorMessage = "Cep must be at most 8 characters long"), 
    MinLength(8, ErrorMessage = "Cep must be at least 8 characters long")]
    public string Cep { get; set; } = string.Empty;

    public string? NickName { get; set; }

    public string? Image { get; set; }
}
