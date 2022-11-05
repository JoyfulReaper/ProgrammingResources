using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using ProgrammingResources.ApiClient.Interface;

namespace ProgrammingResources.ApiClient;
public class TypeEndpoint : EndpointBase, ITypeEndpoint
{
    private readonly HttpClient _client;

    public TypeEndpoint(HttpClient client)
    {
        _client = client;
    }

    public async Task<IEnumerable<Type>> GetAll()
    {
        var types = await _client.GetFromJsonAsync<IEnumerable<Type>>($"api/v1/Type");
        ThrowIfNull(types);

        return types!;
    }

    public async Task Add(string type)
    {
        using var response = await _client.PutAsJsonAsync("api/v1/Type", type);
        CheckResponse(response);
    }

    public async Task Delete(string type)
    {
        using var response = await _client.DeleteAsync($"api/v2/Type/{type}");
        CheckResponse(response);
    }
}
