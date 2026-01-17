namespace RedeSocial.Application.Contracts.Documents.Dtos;

public class UserDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateOnly BirthDate { get; set; }
    public string Cep { get; set; } = string.Empty;
    public string? Image { get; set; }
    public string? NickName { get; set; }
    public int PostsCount { get; set; }
    public int FriendsCount { get; set; }
}
