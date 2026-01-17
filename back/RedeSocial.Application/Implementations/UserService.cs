using RedeSocial.Application.Contracts;
using RedeSocial.Application.Contracts.Documents.Dtos;
using RedeSocial.Application.Contracts.Documents.Request;
using RedeSocial.Application.Contracts.Documents.Response;
using RedeSocial.Application.Mappers;
using RedeSocial.Application.Validations;
using RedeSocial.Domain.Contracts.Documents.Request;
using RedeSocial.Domain.Contracts.Repositories;

namespace RedeSocial.Application.Implementations;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IFriendRepository _friendRepository;
    private readonly IPostRepository _postRepository;
    private readonly IUserValidation _userValidation;

    public UserService
        (
        IUserRepository userRepository, 
        IFriendRepository friendRepository, 
        IPostRepository postRepository, 
        IUserValidation userValidation
        )
    {
        _userRepository = userRepository;
        _friendRepository = friendRepository;
        _postRepository = postRepository;
        _userValidation = userValidation;
    }

    public async Task<LoginResponse> Login(LoginRequest loginRequest)
    {
        var response = new LoginResponse();

        var user = await _userRepository.GetByEmail(loginRequest.Email);

        if (user is null)
        {
            response.AddNotification(new Notification("Email or password invalid"));
            return response;
        }

        if (!user.ValidatePassword(loginRequest.Password))
        {
            response.AddNotification(new Notification("Email or password invalid"));
            return response;
        }

        response.LoginDto = LoginMapper.MapToLoginDto(user);

        return response;
    }

    public async Task<GenericResponse> RegisterUser(RegisterRequest request)
    {
        var response = new GenericResponse();

        if (!await _userValidation.IsEmailUnique(request.Email))
        {
            response.AddNotification(new Notification("User already exists with this email"));
            return response;
        }

        if (!_userValidation.IsOlderThan16(request.BirthDate))
        {
            response.AddNotification(new Notification("User must be at least 16 years old"));
            return response;
        }

        if (!await _userValidation.IsCepValid(request.Cep))
        {
            response.AddNotification(new Notification("CEP not found"));
            return response;
        }

        var user = UserMapper.MapToUser(request);
        user.SetPassword(request.Password);

        await _userRepository.AddUser(user);

        response.Success = true;
        return response;
    }

    public async Task<UserResponse> GetUser(string email)
    {
        var response = new UserResponse();

        var user = await _userRepository.GetByEmail(email);

        if (user is null)
        {
            response.AddNotification(new Notification("User not found"));
            return response;
        }

        var userDto = UserMapper.MapToUserDto(user);
        userDto.PostsCount = user.Posts?.Count ?? 0;
        userDto.FriendsCount = user.GetAllFriends()?.Count() ?? 0;

        response.User = userDto;
        return response;

    }

    public async Task<GenericResponse> UpdateUser(UpdateUserRequest request, string email)
    {
        var response = new GenericResponse();

        var user = await _userRepository.GetByEmail(email);
        if (user is null)
        {
            response.AddNotification(new Notification("User not found"));
            return response;
        }

        if (request.Nickname == null &&
            request.Image == null &&
            request.Password == null)
        {
            response.AddNotification(new Notification("No data to update"));
            return response;
        }

        if (request.Nickname != null)
        {
            if (request.Nickname.Length < 2)
            {
                response.AddNotification(new Notification("Nickname must have at least 2 characters"));
                return response;
            }

            user.UpdateNickname(request.Nickname);
        }

        if (request.Image != null)
        {
            user.UpdateImage(request.Image);
        }

        if (request.Password != null)
        {
            if (request.Password.Length > 128 && request.Password.Length < 3)
            {
                response.AddNotification(new Notification("Password must have at least 8 characters"));
                return response;
            }

            user.SetPassword(request.Password);
        }

        await _userRepository.Update(user);

        response.Success = true;
        return response;
    }

    public async Task<ProfileResponse> GetUserById(int profileId, string userEmail)
    {
        var response = new ProfileResponse();

        var user = await _userRepository.GetByEmail(userEmail);

        if (!await _userValidation.DoesUserExist(profileId))
        {
            response.AddNotification(new Notification("User profile not found"));
            return response;
        }

        var friendship = await _friendRepository.GetFriendsId(user!.Id);

        bool isFriend = friendship.Contains(profileId);

        var posts = await _postRepository.GetPostsByUserId(profileId, isFriend);

        var postDtos = posts.Select(post => PostMapper.ToPostDto(post));

        var profileUser = await _userRepository.GetById(profileId);

        var profileDto = ProfileMapper.ToProfileDto(profileUser!, postDtos.ToList());

        response.User = profileDto;
        return response;
    }

    public async Task<IEnumerable<SearchUserDto>> SearchUsers(string? search, int page, int limit)
    {
        var skip = (page - 1) * limit;

        var users = await _userRepository.SearchUsers(search, skip, limit);

        return users.Select(UserMapper.ToUserDto);
    }
}