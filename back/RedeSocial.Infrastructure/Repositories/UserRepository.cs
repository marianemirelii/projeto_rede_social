using Microsoft.EntityFrameworkCore;
using RedeSocial.Domain.Contracts.Repositories;
using RedeSocial.Domain.Models;
using RedeSocial.Infrastructure.Data;

namespace RedeSocial.Infrastructure.Repositories;

public class UserRepository : IUserRepository 
{
    private readonly DataContext _context;

    public UserRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByEmail(string email)
    {
        return await _context.Users
        .Include(u => u.Posts)
        .Include(u => u.FriendsRequested)
            .ThenInclude(f => f.FriendUser)
        .Include(u => u.FriendsReceived)
            .ThenInclude(f => f.User)
        .FirstOrDefaultAsync(u => u.Email == email);
    }


    public async Task<bool> ExistsUserByEmail(string email)
    {
        return await _context.Users.AnyAsync(u => u.Email == email);
    }

    public async Task AddUser(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task<User?> GetById(int id)
    {
        return await _context.Users
        .Include(u => u.Posts)
        .Include(u => u.FriendsRequested)
            .ThenInclude(f => f.FriendUser)
        .Include(u => u.FriendsReceived)
            .ThenInclude(f => f.User)
        .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task Update(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<User>> SearchUsers(string? search, int skip, int limit)
    {
        var query = _context.Users.AsQueryable();

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(u => u.Name.ToLower().Contains(search.ToLower()) || 
            u.Email.ToLower().Contains(search.ToLower()));
        }

        return await query.OrderBy(u => u.Name).Skip(skip).Take(limit).ToListAsync();
    }
}
