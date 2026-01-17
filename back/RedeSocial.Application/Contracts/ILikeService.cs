using RedeSocial.Application.Contracts.Documents.Response;

namespace RedeSocial.Application.Contracts;

public interface ILikeService
{
    Task<GenericResponse> LikePost(int postId, string userEmail);
    Task<GenericResponse> UnlikePost(int postId, string userEmail);
}
