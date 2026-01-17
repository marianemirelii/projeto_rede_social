using RedeSocial.Domain.Models;

namespace RedeSocial.Domain.Contracts.Repositories;

public interface IPostRepository
{
    Task Add(Post post);
    Task<IEnumerable<Post>> GetFeedPosts(int userId, IEnumerable<int> friendsIds);
    Task<Post?> PostById(int postId);
    Task<IEnumerable<Post>> GetPostsByUserId(int userId, bool includePrivatePosts);
    }
