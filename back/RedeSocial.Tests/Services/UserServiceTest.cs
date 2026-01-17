using Moq;
using RedeSocial.Application.Contracts;
using RedeSocial.Application.Implementations;
using RedeSocial.Domain.Contracts.Repositories;
using RedeSocial.Domain.Models;
using RedeSocial.Tests.Factores;

namespace RedeSocial.Tests;

public class UserServiceTest
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IFriendRepository> _friendRepositoryMock;
    private readonly Mock<IPostRepository> _postRepositoryMock;
    private readonly Mock<IUserValidation> _userValidation;
    private readonly IUserService _userService;

    public UserServiceTest()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _friendRepositoryMock = new Mock<IFriendRepository>();
        _postRepositoryMock = new Mock<IPostRepository>();
        _userValidation = new Mock<IUserValidation>();

        _userService = new UserService(
            _userRepositoryMock.Object,
            _friendRepositoryMock.Object,
            _postRepositoryMock.Object,
            _userValidation.Object 
        );
    }

    [Fact]
    public async Task LOGIN__DEVE_FAZER_LOGIN_COM_SUCESSO()
    {
        var request = RequestFactores.LoginRequest();

        var user = UserFactore.JoseHenrique();

        _userRepositoryMock.Setup(x => x.GetByEmail(request.Email)).ReturnsAsync(user);

        var response = await _userService.Login(request);

        Assert.True(response.IsValid);
        Assert.NotNull(response.LoginDto);
        
    }

    [Fact]
    public async Task LOGIN__DEVE_RETORNAR_ERRO_QUANDO_EMAIL_NAO_EXISTE_AO_FAZER_LOGIN()
    {
        var request = RequestFactores.LoginRequest();

        var response = await _userService.Login(request);

        Assert.False(response.IsValid);
        Assert.Null(response.LoginDto);
    }

    [Fact]
    public async Task LOGIN__DEVE_RETORNAR_ERRO_QUANDO_SENHA_INVALIDA_AO_FAZER_LOGIN()
    {
        var request = RequestFactores.LoginRequest();
        request.Password = "54321";

        var user = UserFactore.JoseHenrique();

        _userRepositoryMock.Setup(x => x.GetByEmail(request.Email)).ReturnsAsync(user);

        var response = await _userService.Login(request);

        Assert.False(response.IsValid);
        Assert.Null(response.LoginDto);
    }

    [Fact]
    public async Task REGISTER_USER__DEVE_CADASTRAR_USUARIO_COM_SUCESSO()
    {
        var request = RequestFactores.CriarUsuario();

        _userValidation.Setup(x => x.IsEmailUnique(request.Email)).ReturnsAsync(true);
        _userValidation.Setup(x => x.IsOlderThan16(request.BirthDate)).Returns(true);
        _userValidation.Setup(x => x.IsCepValid(request.Cep)).ReturnsAsync(true);

        var response = await _userService.RegisterUser(request);

        Assert.True(response.IsValid);
        _userRepositoryMock.Verify(x => x.AddUser(It.IsAny<User>()), Times.Once);
    }

    [Fact]
    public async Task REGISTER_USER__DEVE_RETORNAR_ERRO_QUANDO_EMAIL_JA_EXISTE()
    {
        var request = RequestFactores.CriarUsuario();

        _userValidation.Setup(x => x.IsEmailUnique(request.Email)).ReturnsAsync(false);

        var response = await _userService.RegisterUser(request);

        Assert.False(response.IsValid);
        _userRepositoryMock.Verify(x => x.AddUser(It.IsAny<User>()), Times.Never);
    }


    [Fact]
    public async Task REGISTER_USER__DEVE_RETORNAR_ERRO_QUANDO_CEP_INVALIDO()
    {
        var request = RequestFactores.CriarUsuarioComCepInvalido();

        _userValidation.Setup(x => x.IsEmailUnique(request.Email)).ReturnsAsync(true);
        _userValidation.Setup(x => x.IsOlderThan16(request.BirthDate)).Returns(true);
        _userValidation.Setup(x => x.IsCepValid(request.Cep)).ReturnsAsync(false);

        var response = await _userService.RegisterUser(request);

        Assert.False(response.IsValid);
        _userRepositoryMock.Verify(x => x.AddUser(It.IsAny<User>()), Times.Never);
    }

    [Fact]
    public async Task REGISTER_USER__DEVE_RETORNAR_ERRO_QUANDO_USUARIO_MENOR_DE_IDADE()
    {
        var request = RequestFactores.CriarUsuarioMenorDeIdade();

        _userValidation.Setup(x => x.IsEmailUnique(request.Email)).ReturnsAsync(true);
        _userValidation.Setup(x => x.IsOlderThan16(request.BirthDate)).Returns(false);
        _userValidation.Setup(x => x.IsCepValid(request.Cep)).ReturnsAsync(true);

        var response = await _userService.RegisterUser(request);

        Assert.False(response.IsValid);
        _userRepositoryMock.Verify(x => x.AddUser(It.IsAny<User>()), Times.Never); 
    }

    [Fact]
    public async Task GET_USER__DEVE_BUSCAR_USUARIO_COM_SUCESSO()
    {
        var user = UserFactore.JoseHenrique();

        _userRepositoryMock.Setup(x => x.GetByEmail(user.Email)).ReturnsAsync(user);

        var response = await _userService.GetUser(user.Email);

        Assert.True(response.IsValid);
        Assert.NotNull(response.User);
    }

    [Fact]
    public async Task GET_USER__DEVE_RETORNAR_ERRO_QUANDO_USUARIO_NAO_EXISTE()
    {
        var user = UserFactore.JoseHenrique();

        _userRepositoryMock.Setup(x => x.GetByEmail(user.Email)).ReturnsAsync((User)null);

        var response = await _userService.GetUser(user.Email);

        Assert.False(response.IsValid);
        Assert.Null(response.User);
    }

    [Theory]
    [InlineData("josehenrique", "URL", "123456")]
    [InlineData("josehenrique", null, null)]
    [InlineData(null, "URL", null)]
    [InlineData(null, null, "123456")]
    public async Task UPDATE_USER__DEVE_ATUALIZAR_USUARIO_COM_SUCESSO(string nickname, string image, string password)
    {
        var request = RequestFactores.atualizarUsuario(nickname, image, password);

        var user = UserFactore.JoseHenrique();

        _userRepositoryMock.Setup(x => x.GetByEmail(user.Email)).ReturnsAsync(user);

        var response = await _userService.UpdateUser(request, user.Email);

        Assert.True(response.Success);
        _userRepositoryMock.Verify(x => x.Update(It.IsAny<User>()), Times.Once);
    }

    [Fact]
    public async Task UPDATE_USER__DEVE_RETORNAR_ERRO_QUANDO_USUARIO_NAO_EXISTE()
    {
        var request = RequestFactores.atualizarUsuario("josehenrique", "URL", "123456");

        _userRepositoryMock.Setup(x => x.GetByEmail(It.IsAny<string>())).ReturnsAsync((User)null);

        var response = await _userService.UpdateUser(request, "josehenrique");

        Assert.False(response.Success);
        _userRepositoryMock.Verify(x => x.Update(It.IsAny<User>()), Times.Never);
    }

    [Theory]
    [InlineData(null, null, null)]
    [InlineData("j", null, null)]
    public async Task UPDATE_USER__DEVE_RETORNAR_ERRO_QUANDO_NAO_HA_DADOS_PARA_ATUALIZAR(string nickname, string image, string password)
    {
        var request = RequestFactores.atualizarUsuario(nickname, image, password);

        var user = UserFactore.JoseHenrique();

        _userRepositoryMock.Setup(x => x.GetByEmail(user.Email)).ReturnsAsync(user);

        var response = await _userService.UpdateUser(request, user.Email);

        Assert.False(response.Success);
        _userRepositoryMock.Verify(x => x.Update(It.IsAny<User>()), Times.Never);
    }

    [Fact]
    public async Task GET_USER_BY_ID__DEVE_BUSCAR_USUARIO_COM_SUCESSO()
    {
        var user = UserFactore.JoseHenrique();
        var friend = UserFactore.MariaSilva();
        int idPerfil = friend.Id;

        _userRepositoryMock.Setup(x => x.GetByEmail(user.Email)).ReturnsAsync(user);
        _userValidation.Setup(x => x.DoesUserExist(idPerfil)).ReturnsAsync(true);
        _friendRepositoryMock.Setup(x => x.GetFriendsId(user.Id)).ReturnsAsync(new List<int> { idPerfil });
        _postRepositoryMock.Setup(x => x.GetPostsByUserId(idPerfil, true)).ReturnsAsync(new List<Post>());
        _userRepositoryMock.Setup(x => x.GetById(idPerfil)).ReturnsAsync(friend);

        var response = await _userService.GetUserById(idPerfil, user.Email);

        Assert.True(response.IsValid);
        Assert.NotNull(response.User);
    }

    [Fact]
    public async Task GET_USER_BY_ID__DEVE_RETORNAR_ERRO_QUANDO_PERFIL_USUARIO_NAO_EXISTE()
    {
        var user = UserFactore.JoseHenrique();
        int idPerfil = 2;

        _userRepositoryMock.Setup(x => x.GetByEmail(user.Email)).ReturnsAsync(user);
        _userValidation.Setup(x => x.DoesUserExist(idPerfil)).ReturnsAsync(false);

        var response = await _userService.GetUserById(idPerfil, user.Email);

        Assert.False(response.IsValid);
    }

    [Fact]
    public async Task SEARCH_USER__DEVE_BUSCAR_USUARIO_COM_SUCESSO()
    {
        var user = UserFactore.JoseHenrique();
        var users = new List<User> { user };

        _userRepositoryMock.Setup(x => x.GetByEmail(user.Email)).ReturnsAsync(user);
        _userRepositoryMock.Setup(x => x.SearchUsers(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(users);

        var response = await _userService.SearchUsers(user.Email, 1, 10);

        Assert.NotNull(response);
        Assert.True(response.Count() > 0);
    }
}