using Moq;
using RedeSocial.Application.Contracts;
using RedeSocial.Application.Implementations;
using RedeSocial.Domain.Contracts.Repositories;
using RedeSocial.Domain.Models;
using RedeSocial.Tests.Factores;

namespace RedeSocial.Tests.Services;

public class PostServiceTest
{
    private readonly Mock<IPostRepository> _postRepositoryMock;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IFriendRepository> _friendRepositoryMock;
    private readonly IPostService _postService;

    public PostServiceTest()
    {
        _postRepositoryMock = new Mock<IPostRepository>();
        _userRepositoryMock = new Mock<IUserRepository>();
        _friendRepositoryMock = new Mock<IFriendRepository>();

        _postService = new PostService(_postRepositoryMock.Object, _userRepositoryMock.Object, _friendRepositoryMock.Object);
    }

    [Fact]
    public async Task CREATE_POST__DEVE_CRIAR_POST_COM_SUCESSO()
    {
        var request = RequestFactores.postRequest();

        var user = UserFactore.JoseHenrique();

        _userRepositoryMock.Setup(x => x.GetByEmail(user.Email)).ReturnsAsync(user);

        var response = await _postService.CreatePost(request, user.Email);

        Assert.True(response.IsValid);
        _postRepositoryMock.Verify(x => x.Add(It.IsAny<Post>()), Times.Once);
    }

    [Fact]
    public async Task GET_ALL_POSTS__DEVE_BUSCAR_POSTS_COM_SUCESSO()
    {
        var user = UserFactore.JoseHenrique();

        _userRepositoryMock.Setup(x => x.GetByEmail(user.Email)).ReturnsAsync(user);
        _friendRepositoryMock.Setup(x => x.GetFriendsId(user.Id)).ReturnsAsync(new List<int>());
        _postRepositoryMock.Setup(x => x.GetPostsByUserId(user.Id, true)).ReturnsAsync(new List<Post>());

        var response = await _postService.GetAllPosts(user.Email);

        Assert.NotNull(response);
    }
}
