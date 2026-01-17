namespace RedeSocial.Application.Contracts.Documents.Dtos;

public class LoginDto
{
    public bool IsAuthenticated { get; set; } = false;
    
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;
}
