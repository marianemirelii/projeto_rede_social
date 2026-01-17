namespace RedeSocial.Application.Contracts.Documents.Dtos;

public class PostDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public bool IsPublic { get; set; }
    public string? Image { get; set; }
    public DateTime CreatedAt { get; set; }
    public int Likes { get; set; }
    public int Comments { get; set; }
}
