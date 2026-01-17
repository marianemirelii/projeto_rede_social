using RedeSocial.Application.Contracts.Documents.Dtos;
using RedeSocial.Domain.Models;

namespace RedeSocial.Application.Mappers;

public static class ProfileMapper
{
    public static ProfileDto ToProfileDto(User user, List<PostDto> posts)
    {
        return new ProfileDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            NickName = user.NickName,
            Image = user.Image,
            FriendsCount = user.GetAllFriends()?.Count() ?? 0,
            PostsCount = posts.Count,
            Posts = posts
        };
    }
}