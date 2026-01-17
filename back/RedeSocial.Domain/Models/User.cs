using System.Security.Cryptography;
using System.Text;
using RedeSocial.Domain.Enum;

namespace RedeSocial.Domain.Models;

public class User
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string? NickName { get; private set; }
    public DateOnly BirthDate { get; private set; }
    public string Cep { get; private set; }
    public string? Image { get; private set; }
    public string PasswordHash { get; private set; }
    public List<Post> Posts { get; private set; } = new();
    public List<Like> Likes { get; private set; } = new();
    public List<Comment> Comments { get; private set; } = new();

    public List<Friend> FriendsRequested { get; private set; } = new();
    public List<Friend> FriendsReceived { get; private set; } = new();


    public User()
    {
    }

    public User(string name, string email, string? nickName, DateOnly birthDate, string cep, string? image)
    {
        Name = name;
        Email = email;
        NickName = nickName;
        BirthDate = birthDate;
        Cep = cep;
        Image = image;
    }

    public void SetPassword(string password)
    {
        PasswordHash = Hash(password);
    }

    public bool ValidatePassword(string password)
    {
        return PasswordHash == Hash(password);
    }

    private static string Hash(string input)
    {
        using var md5Hash = MD5.Create();
        var data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
        var sBuilder = new StringBuilder();

        foreach (var t in data)
        {
            sBuilder.Append(t.ToString("x2"));
        }

        return sBuilder.ToString();
    }

    public void AddPost(Post post)
    {
        Posts.Add(post);
    }

    public void SendFriendRequest(Friend friend)
    {
        FriendsRequested.Add(friend);
    }

    public void AddLike(Like like)
    {
        Likes.Add(like);
    }

    public void AddComment(Comment comment)
    {
        Comments.Add(comment);
    }

    public IEnumerable<User> GetAllFriends()
    {
        return FriendsRequested
            .Where(f => f.Status == Status.Accepted)
            .Select(f => f.FriendUser!)
            .Concat(
                FriendsReceived
                    .Where(f => f.Status == Status.Accepted)
                    .Select(f => f.User!)
            );
    }

    public void UpdateNickname(string nickname)
    {
        NickName = nickname;
    }

    public void UpdateImage(string image)
    {
        Image = image;
    }
}
