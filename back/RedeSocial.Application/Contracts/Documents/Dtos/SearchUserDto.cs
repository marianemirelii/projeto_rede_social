namespace RedeSocial.Application.Contracts.Documents.Dtos;

public class SearchUserDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? NickName { get; set; }
    public string? Image { get; set; }
}
