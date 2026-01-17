using RedeSocial.Application.Contracts.Documents.Dtos;
using RedeSocial.Domain.Models;

namespace RedeSocial.Application.Mappers;

public static class LoginMapper
{
    public static LoginDto MapToLoginDto(User user)
    {
        return new LoginDto
        {
            IsAuthenticated = true,
            Id = user.Id,
            Name = user.Name,
            Email = user.Email
        };
    }
}