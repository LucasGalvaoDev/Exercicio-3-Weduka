using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WedukaEx3.Models;

public class PessoaService
{
    private readonly HttpClient _httpClient;

    public PessoaService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    #region api pessoas
    public async Task<List<Pessoa>> GetPessoasAsync()
    {
        var listaPessoas = await _httpClient.GetFromJsonAsync<List<Pessoa>>("api/pessoas");
        return listaPessoas;
    }

    public async Task<Pessoa> GetPessoaByIdAsync(int id)
    {
        var pessoa = await _httpClient.GetFromJsonAsync<Pessoa>($"api/pessoas/{id}");
        return pessoa;
    }

    public async Task<bool> AddPessoaAsync(Pessoa pessoa)
    {
        var response = await _httpClient.PostAsJsonAsync("api/pessoas", pessoa);
        return response.IsSuccessStatusCode;
    }
    public async Task<bool> UpdatePessoaAsync(Pessoa pessoa)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/pessoas/{pessoa.Id}", pessoa);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeletePessoaAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/pessoas/{id}");
        return response.IsSuccessStatusCode;
    }
    #endregion

    #region api contatos

    public async Task<List<Contato>> GetContatosAsync()
    {
        var contatos = await _httpClient.GetFromJsonAsync<List<Contato>>("api/contatos");
        return contatos;
    }

    public async Task<Contato> GetContatosByIdAsync(int id)
    {
        var contato = await _httpClient.GetFromJsonAsync<Contato>($"api/contatos/{id}");
        return contato;
    }

    public async Task<bool> AddContatosAsync(Contato contato)
    {
        var response = await _httpClient.PostAsJsonAsync("api/contatos", contato);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateContatosAsync(Contato contato)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/contatos/{contato.Id}", contato);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteContatosAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/contatos/{id}");
        return response.IsSuccessStatusCode;
    }

    #endregion
}
