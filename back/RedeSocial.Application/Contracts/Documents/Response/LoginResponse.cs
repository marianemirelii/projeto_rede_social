using RedeSocial.Application.Contracts.Documents.Dtos;
using RedeSocial.Application.Validations;

namespace RedeSocial.Application.Contracts.Documents.Response;

public class LoginResponse : Notifiable
{
    public LoginDto? LoginDto { get; set; }
}
