using RedeSocial.Application.Contracts.Documents.Dtos;
using RedeSocial.Application.Contracts.Documents.Response;
using RedeSocial.Domain.Models;

namespace RedeSocial.Application.Mappers;

public static class FriendMapper
{
    public static FriendDto MapToFriendDto(Friend friend)
    {
        return new FriendDto
        {
            IdFriend = friend.FriendUserId,
            FriendName = friend.FriendUser!.Name,
            FriendEmail = friend.FriendUser.Email,
            Status = friend.Status.ToString()
        };
    }

    public static FriendDto MapToFriendUserDto(Friend friend)
    {
        return new FriendDto
        {
            IdFriend = friend.UserId,
            FriendName = friend.User!.Name,
            FriendEmail = friend.User.Email,
            Status = friend.Status.ToString()
        };
    }
}