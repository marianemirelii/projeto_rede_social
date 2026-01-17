using RedeSocial.Application.Contracts.Documents.Request;
using RedeSocial.Application.Contracts.Documents.Response;

namespace RedeSocial.Application.Contracts;

public interface IFriendService
{
    Task<GenericResponse> Accept(AcceptFriendRequest request, string userEmail);
    Task<GenericResponse> AddFriend(FriendRequest friendRequest, string userEmail);
    Task<List<FriendResponse>> GetAll(string userEmail);
    Task<List<FriendResponse>> GetFriendRequests(string userEmail);
}
