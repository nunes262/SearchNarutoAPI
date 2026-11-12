using System.Net;
using System.Net.Http.Json;
using SearchNarutoAPI.Models;

namespace SearchNarutoAPI.Services;

public class NarutoService
{
    private const int PageSize = 20;

    private readonly HttpClient _http;

    public NarutoService(HttpClient http)
    {
        _http = http;
    }

    // Lista os personagens de forma paginada: GET /characters?page=N&limit=20
    public async Task<CharacterResponse> GetCharactersAsync(int page = 1)
    {
        var response = await _http.GetFromJsonAsync<CharacterResponse>(
            $"characters?page={page}&limit={PageSize}");

        return response ?? new CharacterResponse();
    }

    // Busca personagens pelo nome: GET /characters?name=...
    public async Task<CharacterResponse> SearchByNameAsync(string name)
    {
        var response = await _http.GetFromJsonAsync<CharacterResponse>(
            $"characters?name={Uri.EscapeDataString(name)}&limit=100");

        return response ?? new CharacterResponse();
    }

    // Busca um personagem pelo ID: GET /characters/{id}
    public async Task<Character?> GetByIdAsync(int id)
    {
        var httpResponse = await _http.GetAsync($"characters/{id}");

        if (httpResponse.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }

        httpResponse.EnsureSuccessStatusCode();
        return await httpResponse.Content.ReadFromJsonAsync<Character>();
    }
}
