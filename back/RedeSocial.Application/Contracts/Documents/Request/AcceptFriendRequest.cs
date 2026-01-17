using RedeSocial.Domain.Enum;

namespace RedeSocial.Application.Contracts.Documents.Request;

public class AcceptFriendRequest
{
    public int friendId { get; set; }
    public int status { get; set; }
}
