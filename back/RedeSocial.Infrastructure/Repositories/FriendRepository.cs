using Microsoft.EntityFrameworkCore;
using RedeSocial.Domain.Contracts.Repositories;
using RedeSocial.Domain.Enum;
using RedeSocial.Domain.Models;
using RedeSocial.Infrastructure.Data;

namespace RedeSocial.Infrastructure.Repositories;

public class FriendRepository : IFriendRepository
{
    private readonly DataContext _context;
    
    public FriendRepository(DataContext context)
    {
        _context = context;
    }

    public Task Add(Friend friend)
    {
        _context.Friends.Add(friend);
        return _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsRequest(int userId, int friendId)
    {
        return await _context.Friends.AnyAsync(f =>
        (f.UserId == userId && f.FriendUserId == friendId) ||
        (f.UserId == friendId && f.FriendUserId == userId));
    }

    public async Task<IEnumerable<Friend>> GetFriendsByUserId(int id)
    {
        return await _context.Friends
            .Include(f => f.User)
            .Include(f => f.FriendUser)
            .Where(f => f.UserId == id || f.FriendUserId == id && f.Status == Status.Accepted)
            .ToListAsync();
    }

    public async Task<IEnumerable<int>> GetFriendsId(int id)
    {
        return await _context.Friends
            .Where(f => (f.UserId == id || f.FriendUserId == id) && f.Status == Status.Accepted)
            .Select(f => f.UserId == id ? f.FriendUserId : f.UserId)
            .ToListAsync();
    }

    public Task<Friend?> GetRequest(int id, int friendId)
    {
        return _context.Friends
            .FirstOrDefaultAsync(f => f.UserId == id && f.FriendUserId == friendId ||
                                     f.UserId == friendId && f.FriendUserId == id);
    }

    public async Task<List<Friend>> GetRequestsByUserId(int id)
    {
        return await _context.Friends
            .Include(f => f.User)
            .Include(f => f.FriendUser)
            .Where(f => f.FriendUserId == id && f.Status == Status.Pending)
            .ToListAsync();
    }

    public Task Update(Friend friendRequest)
    {
        _context.Friends.Update(friendRequest);
        return _context.SaveChangesAsync();
    }

    public Task Delete(Friend friendRequest)
    {
        _context.Friends.Remove(friendRequest);
        return _context.SaveChangesAsync();
    }
}
