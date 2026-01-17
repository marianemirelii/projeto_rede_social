using RedeSocial.Application.Contracts;
using RedeSocial.Application.Contracts.Documents.Request;
using RedeSocial.Application.Contracts.Documents.Response;
using RedeSocial.Application.Mappers;
using RedeSocial.Application.Validations;
using RedeSocial.Domain.Contracts.Repositories;
using RedeSocial.Domain.Enum;
using RedeSocial.Domain.Models;

namespace RedeSocial.Application.Implementations;

public class FriendService : IFriendService
{
    private readonly IFriendRepository _friendRepository;
    private readonly IUserRepository _userRepository;
    public FriendService(IFriendRepository friendRepository, IUserRepository userRepository)
    {
        _friendRepository = friendRepository;
        _userRepository = userRepository;
    }

    public async Task<GenericResponse> Accept(AcceptFriendRequest request, string userEmail)
    {
        var response = new GenericResponse();

        var user = await _userRepository.GetByEmail(userEmail);

        var friendRequest = await _friendRepository.GetRequest(user!.Id, request.friendId);

        if (friendRequest == null)
        {
            response.AddNotification(new Notification("Friend request not found"));
            return response;
        }

        if (friendRequest.FriendUserId != user.Id)
        {
            response.AddNotification(new Notification("You cannot respond to this friend request."));
            return response;
        }

        if (friendRequest.Status != Status.Pending)
        {
            response.AddNotification(new Notification("Friend request already responded"));
            return response;
        }

        if(request.status == Status.Accepted.GetHashCode())
        {
            friendRequest.UpdateStatus(Status.Accepted);
        }
        else if(request.status == Status.Rejected.GetHashCode())
        {
            await _friendRepository.Delete(friendRequest);
            response.Success = true;
            return response;
        }
        else
        {
            response.AddNotification(new Notification("Invalid status"));
            return response;
        }

        await _friendRepository.Update(friendRequest);
        response.Success = true;
        return response;
    }

    public async Task<GenericResponse> AddFriend(FriendRequest friendRequest, string userEmail)
    {
        var response = new GenericResponse();

        var user = await _userRepository.GetByEmail(userEmail);
        var friend = await _userRepository.GetById(friendRequest.FriendId);

        if (friend == null)
        {
            response.AddNotification(new Notification("Friend not found"));
            return response;
        }

        if (user!.Id == friend.Id)
        {
            response.AddNotification(new Notification("You can't add yourself as a friend"));
            return response;
        }

        if (await _friendRepository.ExistsRequest(user.Id, friend.Id))
        {
            response.AddNotification(new Notification("Friend request already sent"));
            return response;
        }

        var friendRequestEntity = new Friend(user.Id, user, friend.Id, friend, Status.Pending);

        await _friendRepository.Add(friendRequestEntity);
        response.Success = true;

        return response;
    }

    public async Task<List<FriendResponse>> GetAll(string userEmail)
    {
        var user = await _userRepository.GetByEmail(userEmail);

        var friends = await _friendRepository.GetFriendsByUserId(user!.Id);

        if (friends == null)
        {
            return new List<FriendResponse>();
        }

        var response = friends.Select(f => new FriendResponse
    {
            Friend = f.UserId == user.Id
                ? FriendMapper.MapToFriendDto(f)
                : FriendMapper.MapToFriendUserDto(f)
        }).ToList();

        return response;
    }

    public async Task<List<FriendResponse>> GetFriendRequests(string userEmail)
    {
        var user = await _userRepository.GetByEmail(userEmail);

        var requests = await _friendRepository.GetRequestsByUserId(user!.Id);

        if (requests == null)
        {
            return new List<FriendResponse>();
        }

        var response = requests.Select(r => new FriendResponse
        {
            Friend = FriendMapper.MapToFriendUserDto(r)
        }).ToList();

        return response;
    }
}
