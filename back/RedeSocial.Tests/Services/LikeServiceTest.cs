using Moq;
using RedeSocial.Application.Contracts;
using RedeSocial.Application.Implementations;
using RedeSocial.Domain.Contracts;
using RedeSocial.Domain.Contracts.Repositories;
using RedeSocial.Domain.Models;
using RedeSocial.Tests.Factores;

namespace RedeSocial.Tests.Services;

public class LikeServiceTest
{
    private readonly Mock<ILikeRepository> _likeRepositoryMock;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IPostRepository> _postRepositoryMock;

    private readonly ILikeService _likeService;

    public LikeServiceTest()
    {
        _likeRepositoryMock = new Mock<ILikeRepository>();
        _userRepositoryMock = new Mock<IUserRepository>();
        _postRepositoryMock = new Mock<IPostRepository>();

        _likeService = new LikeService(_likeRepositoryMock.Object, _userRepositoryMock.Object, _postRepositoryMock.Object);
    }

    [Fact]
    public async Task LikePost__DEVE_LIKAR_POST_COM_SUCESSO()
    {
        var user = UserFactore.JoseHenrique();
        int postId = 1;
        var post = PostFactore.Post();

        _userRepositoryMock.Setup(x => x.GetByEmail(user.Email)).ReturnsAsync(user);
        _postRepositoryMock.Setup(x => x.PostById(postId)).ReturnsAsync(post);
        _likeRepositoryMock.Setup(x => x.UserAlreadyLikedPost(postId, user.Id)).ReturnsAsync(false);

        var response = await _likeService.LikePost(postId, user.Email);

        Assert.True(response.Success);
        _likeRepositoryMock.Verify(x => x.LikePost(It.IsAny<Like>()), Times.Once);
    }

    [Fact]
    public async Task LikePost__DEVE_RETORNAR_ERRO_QUANDO_POST_NAO_EXISTE()
    {
        var user = UserFactore.JoseHenrique();
        int postId = 1;

        _userRepositoryMock.Setup(x => x.GetByEmail(user.Email)).ReturnsAsync(user);
        _postRepositoryMock.Setup(x => x.PostById(postId)).ReturnsAsync((Post?)null);

        var response = await _likeService.LikePost(postId, user.Email);

        Assert.False(response.Success);
        Assert.Single(response.Notifications);
        Assert.Equal("Post not found", response.Notifications.First().Message);
    }



    [Fact]
    public async Task LikePost__DEVE_RETORNAR_ERRO_QUANDO_USUARIO_JA_LIKOU_POST()
    {
        var user = UserFactore.JoseHenrique();
        int postId = 1;
        var post = PostFactore.Post();

        _userRepositoryMock.Setup(x => x.GetByEmail(user.Email)).ReturnsAsync(user);
        _postRepositoryMock.Setup(x => x.PostById(postId)).ReturnsAsync(post);
        _likeRepositoryMock.Setup(x => x.UserAlreadyLikedPost(postId, user.Id)).ReturnsAsync(true);

        var response = await _likeService.LikePost(postId, user.Email);

        Assert.False(response.Success);
        Assert.Single(response.Notifications);
        Assert.Equal("You already liked this post", response.Notifications.First().Message);
    }

    [Fact]
    public async Task UnlikePost__DEVE_DESLIKAR_POST_COM_SUCESSO()
    {
        var user = UserFactore.JoseHenrique();
        int postId = 1;
        var post = PostFactore.Post();

        _userRepositoryMock.Setup(x => x.GetByEmail(user.Email)).ReturnsAsync(user);
        _postRepositoryMock.Setup(x => x.PostById(postId)).ReturnsAsync(post);
        _likeRepositoryMock.Setup(x => x.UserAlreadyLikedPost(postId, user.Id)).ReturnsAsync(true);

        var response = await _likeService.UnlikePost(postId, user.Email);

        Assert.True(response.Success);
        _likeRepositoryMock.Verify(x => x.UnlikePost(postId, user.Id), Times.Once);
    }

    [Fact]
    public async Task UnlikePost__DEVE_RETORNAR_ERRO_QUANDO_POST_NAO_EXISTE()
    {
        var user = UserFactore.JoseHenrique();
        int postId = 1;

        _userRepositoryMock.Setup(x => x.GetByEmail(user.Email)).ReturnsAsync(user);
        _postRepositoryMock.Setup(x => x.PostById(postId)).ReturnsAsync((Post?)null);

        var response = await _likeService.UnlikePost(postId, user.Email);

        Assert.False(response.Success);
        Assert.Single(response.Notifications);
        Assert.Equal("Post not found", response.Notifications.First().Message);
    }

    [Fact]
    public async Task UnlikePost__DEVE_RETORNAR_ERRO_QUANDO_USUARIO_NAO_LIKOU_POST()
    {
        var user = UserFactore.JoseHenrique();
        int postId = 1;
        var post = PostFactore.Post();

        _userRepositoryMock.Setup(x => x.GetByEmail(user.Email)).ReturnsAsync(user);
        _postRepositoryMock.Setup(x => x.PostById(postId)).ReturnsAsync(post);
        _likeRepositoryMock.Setup(x => x.UserAlreadyLikedPost(postId, user.Id)).ReturnsAsync(false);

        var response = await _likeService.UnlikePost(postId, user.Email);

        Assert.False(response.Success);
        Assert.Single(response.Notifications);
        Assert.Equal("You didn't like this post", response.Notifications.First().Message);
    }

}
