using RedeSocial.Application.Contracts.Documents.Dtos;
using RedeSocial.Application.Contracts.Documents.Request;
using RedeSocial.Domain.Models;

namespace RedeSocial.Application.Mappers;

public static class UserMapper
{
    public static SearchUserDto ToUserDto(User user)
    {
        return new SearchUserDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            NickName = user.NickName,
            Image = user.Image
        };
    }

    public static User MapToUser(RegisterRequest request)
    {
        return new User
            (
                request.Name, 
                request.Email, 
                request.NickName, 
                request.BirthDate, 
                request.Cep, 
                request.Image
            );
    }

    internal static UserDto MapToUserDto(User user)
    {
        return new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            NickName = user.NickName,
            BirthDate = user.BirthDate,
            Cep = user.Cep,
            Image = user.Image
        };
    }
}
