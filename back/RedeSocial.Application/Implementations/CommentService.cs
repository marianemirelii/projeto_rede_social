using RedeSocial.Application.Contracts;
using RedeSocial.Application.Contracts.Documents.Dtos;
using RedeSocial.Application.Contracts.Documents.Request;
using RedeSocial.Application.Contracts.Documents.Response;
using RedeSocial.Application.Mappers;
using RedeSocial.Application.Validations;
using RedeSocial.Domain.Contracts.Repositories;
using RedeSocial.Domain.Models;

namespace RedeSocial.Application.Implementations;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;
    private readonly IUserRepository _userRepository;
    private readonly IPostRepository _postRepository;

    public CommentService(ICommentRepository commentRepository, IUserRepository userRepository, IPostRepository postRepository)
    {
        _commentRepository = commentRepository;
        _userRepository = userRepository;
        _postRepository = postRepository;
    }

    public async Task<GenericResponse> CommentOnPost(int postId, CommentCreateRequest request, string userEmail)
    {
        var response = new GenericResponse();

        var user = await _userRepository.GetByEmail(userEmail);

        var post = await _postRepository.PostById(postId);

        if (post == null)
        {
            response.Success = false;
            response.AddNotification(new Notification("Post not found"));
            return response;
        }

        var comment = new Comment(postId, post, user!.Id, user, request.Content);

        await _commentRepository.Add(comment);

        response.Success = true;

        return response;
    }

    public async Task<IEnumerable<CommentDto>> GetCommentsByPostId(int postId)
    {
        var comments = await _commentRepository.GetByPostId(postId);

        return comments.Select(CommentMapper.ToDto);
    }
}
