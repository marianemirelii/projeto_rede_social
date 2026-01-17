using RedeSocial.Application.Contracts;
using RedeSocial.Application.Contracts.Documents.Response;
using RedeSocial.Application.Validations;
using RedeSocial.Domain.Contracts;
using RedeSocial.Domain.Contracts.Repositories;
using RedeSocial.Domain.Models;

namespace RedeSocial.Application.Implementations;

public class LikeService : ILikeService
{
    private readonly ILikeRepository _likeRepository;
    private readonly IUserRepository _userRepository;
    private readonly IPostRepository _postRepository;

    public LikeService(ILikeRepository likeRepository, IUserRepository userRepository, IPostRepository postRepository)
    {
        _likeRepository = likeRepository;
        _userRepository = userRepository;
        _postRepository = postRepository;
    }

    public async Task<GenericResponse> LikePost(int postId, string email)
    {
        var response = new GenericResponse();

        var user = await _userRepository.GetByEmail(email);

        var post = await _postRepository.PostById(postId);
        if (post == null)
        {
            response.Success = false;
            response.AddNotification(new Notification("Post not found"));
            return response;
        }

        var alreadyLiked = await _likeRepository.UserAlreadyLikedPost(postId, user!.Id);
        if (alreadyLiked)
        {
            response.Success = false;
            response.AddNotification(new Notification("You already liked this post"));
            return response;
        }

        Like like = new Like(postId, post, user.Id, user);

        await _likeRepository.LikePost(like);
        response.Success = true;
        return response;
    }

    public async Task<GenericResponse> UnlikePost(int postId, string email)
    {
        var response = new GenericResponse();

        var user = await _userRepository.GetByEmail(email);

        var post = await _postRepository.PostById(postId);
        if (post == null)
        {
            response.Success = false;
            response.AddNotification(new Notification("Post not found"));
            return response;
        }

        var alreadyLiked = await _likeRepository.UserAlreadyLikedPost(postId, user!.Id);
        if (!alreadyLiked)
        {
            response.Success = false;
            response.AddNotification(new Notification("You didn't like this post"));
            return response;
        }

        await _likeRepository.UnlikePost(postId, user.Id);
        response.Success = true;
        return response;
    }
}
