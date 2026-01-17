using RedeSocial.Application.Contracts;
using RedeSocial.Application.Contracts.Documents.Dtos;
using RedeSocial.Application.Contracts.Documents.Request;
using RedeSocial.Application.Contracts.Documents.Response;
using RedeSocial.Application.Mappers;
using RedeSocial.Application.Validations;
using RedeSocial.Domain.Contracts.Repositories;
using RedeSocial.Domain.Models;

namespace RedeSocial.Application.Implementations;

public class PostService : IPostService
{
    private readonly IPostRepository _postRepository;
    private readonly IUserRepository _userRepository;
    private readonly IFriendRepository _friendRepository;

    public PostService(IPostRepository postRepository, IUserRepository userRepository, IFriendRepository friendRepository)
    {
        _postRepository = postRepository;
        _userRepository = userRepository;
        _friendRepository = friendRepository;
    }


    public async Task<PostCreateResponse> CreatePost(PostCreateRequest request, string email)
    {
        var response = new PostCreateResponse();

        var user = await _userRepository.GetByEmail(email);

        Post post = new Post(user!.Id, request.Content, request.IsPublic, request.Image, user);

        await _postRepository.Add(post);

        PostDto postDto = PostMapper.ToPostDto(post);

        response.PostDto = postDto;
        return response;
    }

    public async Task<IEnumerable<PostDto>> GetAllPosts(string userEmail)
    {
        var user = await _userRepository.GetByEmail(userEmail);

        var friendsIds = await _friendRepository.GetFriendsId(user!.Id);

        var posts = await _postRepository.GetFeedPosts(user.Id, friendsIds);

        var postDtos = posts.Select(post => PostMapper.ToPostDto(post));

        return postDtos;
    }
}
