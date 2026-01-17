using Microsoft.EntityFrameworkCore;
using RedeSocial.Domain.Contracts;
using RedeSocial.Domain.Models;
using RedeSocial.Infrastructure.Data;

namespace RedeSocial.Infrastructure.Repositories;

public class LikeRepository : ILikeRepository
{
    private readonly DataContext _context;

    public LikeRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<bool> UserAlreadyLikedPost(int postId, int userId)
    {
        return await _context.Likes.AnyAsync(l => l.PostId == postId && l.UserId == userId);
    }

    public Task LikePost(Like like)
    {
        _context.Likes.Add(like);
        return _context.SaveChangesAsync();
    }

    public Task UnlikePost(int postId, int id)
    {
        _context.Likes.RemoveRange(_context.Likes.Where(l => l.PostId == postId && l.UserId == id));
        return _context.SaveChangesAsync();
    }
}
