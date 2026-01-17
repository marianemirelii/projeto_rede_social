namespace RedeSocial.Application.Contracts.Documents.Request;

public class UpdateUserRequest
{
    public string? Nickname { get; set; }
    public string? Image { get; set; }
    public string? Password { get; set; }
}
