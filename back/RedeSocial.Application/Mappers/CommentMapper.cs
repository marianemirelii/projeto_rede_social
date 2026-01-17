using RedeSocial.Application.Contracts.Documents.Dtos;
using RedeSocial.Domain.Models;

namespace RedeSocial.Application.Mappers;

public static class CommentMapper
{
    public static CommentDto ToDto(Comment comment)
    {
        return new CommentDto
        {
            Id = comment.Id,
            PostId = comment.PostId,
            UserId = comment.UserId,
            ImageUser = comment.User!.Image,
            UserName = comment.User!.Name,
            Content = comment.Content,
            CreatedAt = comment.CreatedAt
        };
    }
}