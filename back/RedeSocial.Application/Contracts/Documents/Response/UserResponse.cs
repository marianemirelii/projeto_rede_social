using RedeSocial.Application.Contracts.Documents.Dtos;
using RedeSocial.Application.Validations;

namespace RedeSocial.Application.Contracts.Documents.Response;

public class UserResponse : Notifiable
{
    public UserDto? User { get; set; }
}
