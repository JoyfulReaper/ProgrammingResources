using ProgrammingResources.ApiClient.Interface;
using ProgrammingResources.ApiClient.Models;
using System.Net.Http.Json;

namespace ProgrammingResources.ApiClient;
public class ResourceEndpoint : EndpointBase, IResourceEndpoint
{
    private readonly HttpClient _client;

    public ResourceEndpoint(HttpClient client)
    {
        _client = client;
    }

    public async Task<Resource> Add(Resource resource)
    {
        using var response = await _client.PostAsJsonAsync($"/api/v1/Resource", resource);
        CheckResponse(response);

        var output = await response.Content.ReadFromJsonAsync<Resource>();
        ThrowIfNull(output);

        return output!;
    }

    public async Task<Resource> Get(int resourceId)
    {
        var resource = await _client.GetFromJsonAsync<Resource>($"api/v1/Resource/{resourceId}");
        ThrowIfNull(resource);

        return resource!;
    }

    public async Task<IEnumerable<Resource>> GetAll()
    {
        var resources = await _client.GetFromJsonAsync<IEnumerable<Resource>>("api/v1/Resource");
        ThrowIfNull(resources);

        return resources!;
    }
}
