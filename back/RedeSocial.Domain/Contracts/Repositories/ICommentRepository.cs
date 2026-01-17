using RedeSocial.Domain.Models;

namespace RedeSocial.Domain.Contracts.Repositories;

public interface ICommentRepository
{
    Task Add(Comment comment);

    Task<List<Comment>> GetByPostId(int postId);
}
