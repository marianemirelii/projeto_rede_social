using RedeSocial.Domain.Enum;

namespace RedeSocial.Application.Contracts.Documents.Dtos;

public class FriendDto
{
    public int IdFriend { get; set; }
    public string FriendName { get; set; } = string.Empty;
    public string FriendEmail { get; set; } = string.Empty;
    public string? ImageUrl { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
}
