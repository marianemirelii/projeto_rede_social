using RedeSocial.Application.Contracts.Documents.Dtos;
using RedeSocial.Application.Contracts.Documents.Request;
using RedeSocial.Application.Contracts.Documents.Response;

namespace RedeSocial.Application.Contracts;

public interface IPostService
{
    Task<PostCreateResponse> CreatePost(PostCreateRequest request, string userEmail);
    Task<IEnumerable<PostDto>> GetAllPosts(string userEmail);
}
