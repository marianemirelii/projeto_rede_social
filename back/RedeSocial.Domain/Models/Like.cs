namespace RedeSocial.Domain.Models;

public class Like
{
    public int Id { get; set; }
    public int PostId { get; set; }
    public Post? Post { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
    public DateTime CreatedAt { get; set; }

    public Like()
    {
    }

    public Like(int postId, Post? post, int userId, User? user)
    {
        PostId = postId;
        Post = post;
        UserId = userId;
        User = user;
        CreatedAt = DateTime.UtcNow;
    }
}
