using System.ComponentModel.DataAnnotations;

namespace RedeSocial.Application.Contracts.Documents.Request;

public class CommentCreateRequest
{

    [Required(ErrorMessage = "Content is required")]
    public string Content { get; set; } = string.Empty;
}
