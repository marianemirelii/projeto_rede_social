using RedeSocial.Application.Contracts.Documents.Dtos;
using RedeSocial.Application.Contracts.Documents.Request;
using RedeSocial.Application.Contracts.Documents.Response;
using RedeSocial.Domain.Contracts.Documents.Request;

namespace RedeSocial.Application.Contracts;

public interface IUserService
{
    Task<UserResponse> GetUser(string email);
    Task<ProfileResponse> GetUserById(int profileId, string userEmail);
    Task<GenericResponse> RegisterUser(RegisterRequest registerRequest);
    Task<GenericResponse> UpdateUser(UpdateUserRequest request, string email);
    Task<LoginResponse> Login(LoginRequest loginRequest);
    Task<IEnumerable<SearchUserDto>> SearchUsers(string? search, int page, int limit);
}
