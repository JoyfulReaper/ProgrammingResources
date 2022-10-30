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

    public async Task<ProgrammingLanguage> Get(int programmingLanguageId)
    {
        var programmingLangauge = await _client.GetFromJsonAsync<ProgrammingLanguage>($"api/v1/ProgrammingLanguage/{programmingLanguageId}");
        ThrowIfNull(programmingLangauge);

        return programmingLangauge!;
    }

    public async Task<IEnumerable<ProgrammingLanguage>> GetAll()
    {
        var programmingLanguages = await _client.GetFromJsonAsync<IEnumerable<ProgrammingLanguage>>($"api/v1/ProgrammingLanguage");
        ThrowIfNull(programmingLanguages);

        return programmingLanguages!;
    }

    public async Task Add(ProgrammingLanguage programmingLanguage)
    {
        using var response = await _client.PutAsJsonAsync("api/v1/ProgrammingLangauge", programmingLanguage);
        CheckResponse(response);
    }

    public async Task Delete(int programmingLangaugeId)
    {
        using var response = await _client.DeleteAsync($"api/v2/ProgrammingLangauge/{programmingLangaugeId}");
        CheckResponse(response);
    }
}
