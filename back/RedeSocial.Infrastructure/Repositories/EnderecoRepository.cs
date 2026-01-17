using RedeSocial.Domain.Contracts.Repositories;

namespace RedeSocial.Infrastructure.Repositories;

public class EnderecoRepository : IEnderecoRepository
{
    private readonly HttpClient _httpClient;

    public EnderecoRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> ExistsCep(string cep)
    {
        var requestUri = $"/ws/{cep}/json";

        var response = await _httpClient.GetAsync(requestUri);

        var erro = new { erro = true };

        if(response.Content.ReadAsStringAsync().Result.Contains("erro"))
        {
            return false;
        }

        return true;
    }
}
