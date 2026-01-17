namespace RedeSocial.Application.Contracts.Documents.Dtos;

public class ProfileDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Image { get; set; }
    public string? NickName { get; set; }
    public int FriendsCount { get; set; }
    public int PostsCount { get; set; }
    public List<PostDto> Posts { get; set; } = new List<PostDto>();
}
