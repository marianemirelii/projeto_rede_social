using RedeSocial.Domain.Models;

namespace RedeSocial.Domain.Contracts.Repositories;

public interface IUserRepository
{

    Task<User?> GetByEmail(string email);

    Task<bool> ExistsUserByEmail(string email);

    Task AddUser(User user);

    Task<User?> GetById(int id);

    Task Update(User user);
    
    Task<IEnumerable<User>> SearchUsers(string? search, int skip, int limit);
}
