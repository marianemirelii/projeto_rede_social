using RedeSocial.Domain.Contracts.Repositories;

namespace RedeSocial.Domain.Validations;

public class UserValidation : IUserValidation
{
    private readonly IUserRepository _userRepository;
    private readonly IEnderecoRepository _enderecoRepository;

    public UserValidation(IUserRepository userRepository, IEnderecoRepository enderecoRepository)
    {
        _userRepository = userRepository;
        _enderecoRepository = enderecoRepository;
    }

    public async Task<bool> IsEmailUnique(string email)
    {
        return !await _userRepository.ExistsUserByEmail(email);
    }

    public bool IsOlderThan16(DateOnly birthDate)
    {
        var today = DateOnly.FromDateTime(DateTime.Today);
        var minimumDate = today.AddYears(-16);

        return birthDate <= minimumDate;
    }

    public async Task<bool> IsCepValid(string cep)
    {
        return await _enderecoRepository.ExistsCep(cep);
    }

    public async Task<bool> DoesUserExist(int userId)
    {
        var user = await _userRepository.GetById(userId);
        return user is not null;
    }

}
