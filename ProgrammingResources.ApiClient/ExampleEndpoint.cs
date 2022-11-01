

using ProgrammingResources.ApiClient.Interface;
using ProgrammingResources.ApiClient.Models;
using System.Net.Http.Json;

namespace ProgrammingResources.ApiClient;
public class ExampleEndpoint : EndpointBase, IExampleEndpoint
{
    private readonly HttpClient _client;

    public ExampleEndpoint(HttpClient client)
    {
        _client = client;
    }

    public async Task<Example> Get(int exampleId)
    {
        var example = await _client.GetFromJsonAsync<Example>($"api/v1/Example/{exampleId}");
        ThrowIfNull(example);

        return example!;
    }

    public async Task<IEnumerable<Example>> GetForResource(int resourceId)
    {
        var examples = await _client.GetFromJsonAsync<IEnumerable<Example>>($"api/v1/Example/resource/{resourceId}");
        ThrowIfNull(examples);

        return examples!;
    }

    public async Task<Example> Add(Example example)
    {
        using var response = await _client.PostAsJsonAsync($"api/v1/Example", example);
        CheckResponse(response);

        var output = await response.Content.ReadFromJsonAsync<Example>();
        ThrowIfNull(output);

        return output!;
    }

    public async Task Delete(int exampleId)
    {
        using var response = await _client.DeleteAsync($"api/v1/Example/{exampleId}");
        CheckResponse(response);
    }
}
