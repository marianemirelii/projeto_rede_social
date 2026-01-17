using RedeSocial.Application.Contracts.Documents.Request;
using RedeSocial.Domain.Contracts.Documents.Request;

namespace RedeSocial.Tests.Factores;

public static class RequestFactores
{
    public static LoginRequest LoginRequest()
    {
        return new LoginRequest
        {
            Email = "jose.henrique@example.com",
            Password = "12345"
        };
    }

    public static RegisterRequest CriarUsuario()
    {
        return new RegisterRequest
        {
            Name = "Jose Henrique",
            Email = "jose.henrique@example.com",
            Password = "12345",
            BirthDate = new DateOnly(1990, 5, 20),
            Cep = "58808583"
        };
    }

    public static RegisterRequest CriarUsuarioComCepInvalido()
    {
        return new RegisterRequest
        {
            Name = "Maria Silva",
            Email = "maria.silva@example.com",
            Password = "12345",
            BirthDate = new DateOnly(1990, 5, 20),
            Cep = "12345678"
        };
    }

    public static RegisterRequest CriarUsuarioMenorDeIdade()
    {
        return new RegisterRequest
        {
            Name = "Maria Silva",
            Email = "maria.silva@example.com",
            Password = "12345",
            BirthDate = new DateOnly(2015, 5, 20),
            Cep = "58808583"
        };
    }

    public static UpdateUserRequest atualizarUsuario(string? nickname, string? image, string? password)
    {
        return new UpdateUserRequest
        {
            Nickname = nickname,
            Image = image,
            Password = password
        };
    }


    public static PostCreateRequest postRequest()
    {
        return new PostCreateRequest
        {
            Content = "Teste",
            IsPublic = true,
            Image = "URL"
        };
    }

    public static AcceptFriendRequest acceptFriendRequest()
    {
        return new AcceptFriendRequest
        {
            friendId = 1,
            status = 2
        };
    }

    public static AcceptFriendRequest RejectFriendRequest()
    {
        return new AcceptFriendRequest
        {
            friendId = 1,
            status = 3
        };
    }
}
