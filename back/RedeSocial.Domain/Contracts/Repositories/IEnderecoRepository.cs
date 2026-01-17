namespace RedeSocial.Domain.Contracts.Repositories;

public interface IEnderecoRepository
{
    Task<bool> ExistsCep(string cep);
}
