using RedeSocial.Application.Validations;

namespace RedeSocial.Application.Contracts.Documents.Response;

public class GenericResponse : Notifiable
{
    public bool Success { get; set; }
}
