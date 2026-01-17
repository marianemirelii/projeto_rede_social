using RedeSocial.Domain.Models;

namespace RedeSocial.Domain.Contracts.Repositories;

public interface IFriendRepository
{
    Task Add(Friend friend);

    Task<bool> ExistsRequest(int userId, int friendId);
    Task<IEnumerable<Friend>> GetFriendsByUserId(int id);
    Task<Friend?> GetRequest(int id, int friendId);
    Task<List<Friend>> GetRequestsByUserId(int id);
    Task Update(Friend friendRequest);
    Task<IEnumerable<int>> GetFriendsId(int id);
    Task Delete(Friend friendRequest);
}
