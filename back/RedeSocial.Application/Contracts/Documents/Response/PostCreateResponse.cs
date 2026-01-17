using RedeSocial.Application.Contracts.Documents.Dtos;
using RedeSocial.Application.Validations;

namespace RedeSocial.Application.Contracts.Documents.Response;

public class PostCreateResponse : Notifiable
{
    public PostDto? PostDto { get; set; }
}