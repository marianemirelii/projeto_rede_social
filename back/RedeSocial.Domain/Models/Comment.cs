
namespace RedeSocial.Domain.Models;

public class Comment
{
    public int Id { get; private set; }
    public int PostId { get; private set; }
    public Post? Post { get; private set; }
    public int UserId { get; private set; }
    public User? User { get; private set; }
    public string Content { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public Comment()
    {
    }

    public Comment(int postId, Post? post, int userId, User? user, string content)
    {
        PostId = postId;
        Post = post;
        UserId = userId;
        User = user;
        Content = content;
        CreatedAt = DateTime.UtcNow;
    }
}
