namespace RedeSocial.Domain.Models;

public class Post
{
    public int Id { get; private set; }
    public int UserId { get; private set; }
    public User User { get; private set; }
    public string Content { get; private set; }
    public bool IsPublic { get; private set; }
    public string? Image { get; private set; }
    public List<Like> Likes { get; private set; } = new();
    public List<Comment> Comments { get; private set; } = new();
    public DateTime CreatedAt { get; private set; }

    public Post()
    {
    }

    public Post(int userId, string content, bool isPublic, string? image, User user)
    {
        UserId = userId;
        User = user;
        Content = content;
        IsPublic = isPublic;
        Image = image;
        CreatedAt = DateTime.UtcNow;
    }

    public void AddLike(Like like)
    {
        Likes.Add(like);
    }

    public void AddComment(Comment comment)
    {
        Comments.Add(comment);
    }
}
