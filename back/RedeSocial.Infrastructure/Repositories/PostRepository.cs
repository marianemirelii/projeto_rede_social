using Microsoft.EntityFrameworkCore;
using RedeSocial.Domain.Contracts.Repositories;
using RedeSocial.Domain.Models;
using RedeSocial.Infrastructure.Data;

namespace RedeSocial.Infrastructure.Repositories;

public class PostRepository : IPostRepository
{
    private readonly DataContext _context;

    public PostRepository(DataContext context)
    {
        _context = context;
    }

    public Task Add(Post post)
    {
        _context.Posts.Add(post);
        return _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Post>> GetFeedPosts(int userId, IEnumerable<int> friendsIds)
    {
        return await _context.Posts
        .Where(p =>
            p.IsPublic ||
            p.UserId == userId ||
            friendsIds.Contains(p.UserId)
        )
        .OrderByDescending(p => p.CreatedAt)
        .Include(p => p.User)
        .Include(p => p.Likes)
        .Include(p => p.Comments)
        .ToListAsync();
    }

    public async Task<IEnumerable<Post>> GetPostsByUserId(int userId, bool includePrivatePosts)
    {
        return await _context.Posts
            .Where(p => p.UserId == userId && (includePrivatePosts || p.IsPublic))
            .OrderByDescending(p => p.CreatedAt)
            .Include(p => p.User)
            .Include(p => p.Likes)
            .Include(p => p.Comments)
            .ToListAsync();
    }

    public async Task<Post?> PostById(int postId)
    {
        return await _context.Posts.FirstOrDefaultAsync(p => p.Id == postId);
    }
}