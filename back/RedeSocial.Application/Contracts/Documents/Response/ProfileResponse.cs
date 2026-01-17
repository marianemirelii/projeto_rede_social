using RedeSocial.Application.Contracts.Documents.Dtos;
using RedeSocial.Application.Validations;

namespace RedeSocial.Application.Contracts.Documents.Response;

public class ProfileResponse : Notifiable
{
    public ProfileDto User { get; set; } = new ProfileDto();
}
