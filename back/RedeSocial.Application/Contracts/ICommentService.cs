using RedeSocial.Application.Contracts.Documents.Dtos;
using RedeSocial.Application.Contracts.Documents.Request;
using RedeSocial.Application.Contracts.Documents.Response;

namespace RedeSocial.Application.Contracts;

public interface ICommentService
{
    Task<GenericResponse> CommentOnPost(int postId, CommentCreateRequest commentCreateRequest, string userEmail);
    Task<IEnumerable<CommentDto>> GetCommentsByPostId(int postId);
}
