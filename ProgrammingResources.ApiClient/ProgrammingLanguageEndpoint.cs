using ProgrammingResources.ApiClient.Interface;
using ProgrammingResources.ApiClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingResources.ApiClient;

public class ProgrammingLanguageEndpoint : EndpointBase, IProgrammingLanguageEndpoint
{
    private readonly HttpClient _client;

    public ProgrammingLanguageEndpoint(HttpClient client)
    {
        _client = client;
    }

    public async Task<IEnumerable<string>> GetAll()
    {
        var programmingLanguages = await _client.GetFromJsonAsync<IEnumerable<string>>($"api/v1/ProgrammingLanguage");
        ThrowIfNull(programmingLanguages);

        return programmingLanguages!;
    }

    public async Task Add(string programmingLanguage)
    {
        using var response = await _client.PutAsJsonAsync($"api/v1/ProgrammingLangauge", programmingLanguage);
        CheckResponse(response);
    }

    public async Task Delete(string programmingLangauge)
    {
        using var response = await _client.DeleteAsync($"api/v1/ProgrammingLangauge/{programmingLangauge}");
        CheckResponse(response);
    }
}
