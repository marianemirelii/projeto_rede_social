using Moq;
using RedeSocial.Application.Contracts;
using RedeSocial.Application.Contracts.Documents.Request;
using RedeSocial.Application.Implementations;
using RedeSocial.Domain.Contracts.Repositories;
using RedeSocial.Domain.Enum;
using RedeSocial.Domain.Models;
using RedeSocial.Tests.Factores;

namespace RedeSocial.Tests.Services;

public class FriendServiceTest
{
    private readonly Mock<IFriendRepository> _friendRepositoryMock;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly IFriendService _friendService;

    public FriendServiceTest()
    {
        _friendRepositoryMock = new Mock<IFriendRepository>();
        _userRepositoryMock = new Mock<IUserRepository>();
        _friendService = new FriendService(_friendRepositoryMock.Object, _userRepositoryMock.Object);
    }

    [Fact]
    public async Task Accept__DEVE_ACEITAR_AMIZADE_COM_SUCESSO()
    {
        var user1 = UserFactore.JoseHenrique();
        var user2 = UserFactore.MariaSilva();

        var friend = new Friend(
            user1.Id, user1,
            user2.Id, user2,
            Status.Pending
        );

        var request = new AcceptFriendRequest
        {
            friendId = user1.Id,
            status = Status.Accepted.GetHashCode()
        };

        _userRepositoryMock
            .Setup(x => x.GetByEmail(user2.Email))
            .ReturnsAsync(user2);

        _friendRepositoryMock
            .Setup(x => x.GetRequest(user2.Id, user1.Id))
            .ReturnsAsync(friend);

        var response = await _friendService.Accept(request, user2.Email);

        Assert.True(response.Success);
        _friendRepositoryMock.Verify(x => x.Update(It.IsAny<Friend>()), Times.Once);
    }

    [Fact]
    public async Task Accept__DEVE_RETORNAR_ERRO_QUANDO_PEDIDO_DE_AMIZADE_NAO_EXISTE()
    {
        var request = RequestFactores.acceptFriendRequest();

        var User = UserFactore.JoseHenrique();

        _userRepositoryMock.Setup(x => x.GetByEmail(It.IsAny<string>())).ReturnsAsync(User);

        var response = await _friendService.Accept(request, User.Email);

        Assert.False(response.Success);
        Assert.Single(response.Notifications);
        Assert.Equal("Friend request not found", response.Notifications.First().Message);
    }

    [Fact]
    public async Task Accept__DEVE_RETORNAR_ERRO_QUANDO_USUARIO_JA_RESPONDEU()
    {
        var user1 = UserFactore.JoseHenrique();
        var user2 = UserFactore.MariaSilva();

        var friend = new Friend(
            user1.Id,
            user1,
            user2.Id,
            user2,
            Status.Accepted
        );

        var request = RequestFactores.acceptFriendRequest();

        _userRepositoryMock
            .Setup(x => x.GetByEmail(user2.Email))
            .ReturnsAsync(user2);

        _friendRepositoryMock
            .Setup(x => x.GetRequest(user2.Id, user1.Id))
            .ReturnsAsync(friend);

        var response = await _friendService.Accept(request, user2.Email);

        Assert.False(response.Success);
        Assert.Single(response.Notifications);
        Assert.Equal(
            "Friend request already responded",
            response.Notifications.First().Message
        );
    }

    [Fact]
    public async Task Accept__DEVE_REJEITAR_AMIZADE_COM_SUCESSO()
    {
        var user1 = UserFactore.JoseHenrique();   
        var user2 = UserFactore.MariaSilva();   

        var friend = new Friend(
            user1.Id,
            user1,
            user2.Id,
            user2,
            Status.Pending
        );

        var request = RequestFactores.RejectFriendRequest();

        _userRepositoryMock
            .Setup(x => x.GetByEmail(user2.Email))
            .ReturnsAsync(user2);

        _friendRepositoryMock
            .Setup(x => x.GetRequest(user2.Id, user1.Id))
            .ReturnsAsync(friend);

        var response = await _friendService.Accept(request, user2.Email);

        Assert.True(response.Success);
        _friendRepositoryMock.Verify(x => x.Delete(friend), Times.Once);
    }

    [Fact]
    public async Task AddFriend__DEVE_ADICIONAR_AMIZADE_COM_SUCESSO()
    {
        var user = UserFactore.JoseHenrique();
        var user2 = UserFactore.MariaSilva();
        var request = new FriendRequest
        {
            FriendId = user2.Id
        };

        _userRepositoryMock.Setup(x => x.GetByEmail(user.Email)).ReturnsAsync(user);
        _userRepositoryMock.Setup(x => x.GetById(user2.Id)).ReturnsAsync(user2);
        _friendRepositoryMock.Setup(x => x.ExistsRequest(user.Id, user2.Id)).ReturnsAsync(false);

        var response = await _friendService.AddFriend(request, user.Email);

        Assert.True(response.Success);
        _friendRepositoryMock.Verify(x => x.Add(It.IsAny<Friend>()), Times.Once);
    }

    [Fact]
    public async Task AddFriend__DEVE_RETORNAR_ERRO_QUANDO_AMIZADE_JA_EXISTE()
    {
        var user = UserFactore.JoseHenrique();
        var user2 = UserFactore.MariaSilva();
        var request = new FriendRequest
        {
            FriendId = user2.Id
        };

        _userRepositoryMock.Setup(x => x.GetByEmail(user.Email)).ReturnsAsync(user);
        _userRepositoryMock.Setup(x => x.GetById(user2.Id)).ReturnsAsync(user2);
        _friendRepositoryMock.Setup(x => x.ExistsRequest(user.Id, user2.Id)).ReturnsAsync(true);

        var response = await _friendService.AddFriend(request, user.Email);

        Assert.False(response.Success);
        Assert.Single(response.Notifications);
        Assert.Equal("Friend request already sent", response.Notifications.First().Message);
    }

    [Fact]
    public async Task AddFriend__DEVE_RETORNAR_ERRO_QUANDO_AMIGO_NAO_EXISTE()
    {
        var user = UserFactore.JoseHenrique();
        var user2 = UserFactore.MariaSilva();
        var request = new FriendRequest
        {
            FriendId = user2.Id
        };

        _userRepositoryMock.Setup(x => x.GetByEmail(user.Email)).ReturnsAsync(user);
        _userRepositoryMock.Setup(x => x.GetById(user2.Id)).ReturnsAsync((User?)null);

        var response = await _friendService.AddFriend(request, user.Email);

        Assert.False(response.Success);
        Assert.Single(response.Notifications);
        Assert.Equal("Friend not found", response.Notifications.First().Message);
    }

    [Fact]
    public async Task AddFriend__DEVE_RETORNAR_ERRO_QUANDO_USUARIO_TENTA_SER_SEU_PROPRIO_AMIGO_NAO_EXISTE()
    {
        var user = UserFactore.JoseHenrique();
        var request = new FriendRequest
        {
            FriendId = user.Id
        };

        _userRepositoryMock.Setup(x => x.GetByEmail(user.Email)).ReturnsAsync(user);
        _userRepositoryMock.Setup(x => x.GetById(user.Id)).ReturnsAsync(user);

        var response = await _friendService.AddFriend(request, user.Email);

        Assert.False(response.Success);
        Assert.Single(response.Notifications);
        Assert.Equal("You can't add yourself as a friend", response.Notifications.First().Message);
    }
}
