using RedeSocial.Domain.Models;

namespace RedeSocial.Domain.Contracts;

public interface ILikeRepository
{
    Task<bool> UserAlreadyLikedPost(int postId, int userId);
    Task LikePost(Like like);
    Task UnlikePost(int postId, int id);
}
