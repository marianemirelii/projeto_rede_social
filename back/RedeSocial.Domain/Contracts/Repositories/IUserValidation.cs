namespace RedeSocial.Domain.Contracts.Repositories;

public interface IUserValidation
{
    Task<bool> IsEmailUnique(string email);
    bool IsOlderThan16(DateOnly birthDate);
    Task<bool> IsCepValid(string cep);
    Task<bool> DoesUserExist(int id);
}
