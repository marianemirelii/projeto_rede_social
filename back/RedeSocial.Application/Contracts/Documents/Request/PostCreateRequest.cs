using System.ComponentModel.DataAnnotations;

namespace RedeSocial.Application.Contracts.Documents.Request;

public class PostCreateRequest
{
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [MinLength(1, ErrorMessage = "O campo {0} deve ter no mínimo {1} caracteres")]
    [MaxLength(1000, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres")]
    public string Content { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public bool IsPublic { get; set; }

    public string? Image { get; set; }
}
