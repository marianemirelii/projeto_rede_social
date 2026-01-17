using RedeSocial.Application.Contracts.Documents.Dtos;
using RedeSocial.Domain.Models;

namespace RedeSocial.Application.Mappers;

public static class PostMapper
{
    public static PostDto ToPostDto(Post post)
    {
        return new PostDto
        {
            Id = post.Id,
            UserId = post.UserId,
            UserName = post.User.Name,
            ImageUser = post.User.Image,
            Content = post.Content,
            IsPublic = post.IsPublic,
            Image = post.Image,
            CreatedAt = post.CreatedAt,
            Likes = post.Likes?.Count ?? 0,
            Comments = post.Comments?.Count ?? 0
        };
    }
}