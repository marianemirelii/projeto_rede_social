using Moq;
using RedeSocial.Application.Contracts;
using RedeSocial.Application.Contracts.Documents.Request;
using RedeSocial.Application.Implementations;
using RedeSocial.Domain.Contracts.Repositories;
using RedeSocial.Domain.Models;
using RedeSocial.Tests.Factores;

namespace RedeSocial.Tests.Services;

public class CommentServiceTest
{
    private readonly Mock<ICommentRepository> _commentRepositoryMock;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IPostRepository> _postRepositoryMock;
    private readonly ICommentService _commentService;

    public CommentServiceTest()
    {
        _commentRepositoryMock = new Mock<ICommentRepository>();
        _userRepositoryMock = new Mock<IUserRepository>();
        _postRepositoryMock = new Mock<IPostRepository>();

        _commentService = new CommentService(_commentRepositoryMock.Object, _userRepositoryMock.Object, _postRepositoryMock.Object);
    }

    [Fact]
    public  async Task CommentOnPost__DEVE_COMENTAR_POST_COM_SUCESSO()
    {
        var user = UserFactore.JoseHenrique();
        int postId = 1;
        CommentCreateRequest request = new CommentCreateRequest();
        request.Content = "Teste";

        _userRepositoryMock.Setup(x => x.GetByEmail(user.Email)).ReturnsAsync(user);
        _postRepositoryMock.Setup(x => x.PostById(postId)).ReturnsAsync(PostFactore.Post());

        var response = await _commentService.CommentOnPost(postId, request, user.Email);

        Assert.True(response.Success);
        _commentRepositoryMock.Verify(x => x.Add(It.IsAny<Comment>()), Times.Once);

    }

    [Fact]
    public async Task CommentOnPost__DEVE_RETORNAR_ERRO_QUANDO_POST_NAO_EXISTE()
    {
        var user = UserFactore.JoseHenrique();
        int postId = 1;
        CommentCreateRequest request = new CommentCreateRequest();
        request.Content = "Teste";

        _userRepositoryMock.Setup(x => x.GetByEmail(user.Email)).ReturnsAsync(user);
        _postRepositoryMock.Setup(x => x.PostById(postId)).ReturnsAsync((Post?)null);

        var response = await _commentService.CommentOnPost(postId, request, user.Email);

        Assert.False(response.Success);
        Assert.Single(response.Notifications);
        Assert.Equal("Post not found", response.Notifications.First().Message);
    }

    [Fact]
    public async Task GetCommentsByPostId__DEVE_RETORNAR_COMENTARIOS_DO_POST()
    {
        int postId = 1;
        var comment = CommentFactore.Comment();
        var List = new List<Comment>();
        List.Add(comment);
        

        _commentRepositoryMock.Setup(x => x.GetByPostId(postId)).ReturnsAsync(List);

        var response = await _commentService.GetCommentsByPostId(postId);

        Assert.True(response.Any());
    }
}
