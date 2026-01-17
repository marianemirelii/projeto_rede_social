using RedeSocial.Domain.Enum;

namespace RedeSocial.Domain.Models;

public class Friend
{
    public int Id { get; private set; }
    public int UserId { get; private set; }
    public User? User { get; private set; }
    public int FriendUserId { get; private set; }
    public User? FriendUser { get; private set; }
    public Status Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    

    public Friend()
    {
    }

    public Friend(int userId, User? user, int friendUserId, User? friendUser, Status status)
    {
        UserId = userId;
        User = user;
        FriendUserId = friendUserId;
        FriendUser = friendUser;
        Status = status;
        CreatedAt = DateTime.UtcNow;
    }

    public void UpdateStatus(Status newStatus)
    {
        Status = newStatus;
    }
}
