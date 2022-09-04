using ProgrammingResourcesApiClient.Interfaces;
using ProgrammingResourcesApiClient.Models;
using System.Net.Http.Json;

namespace ProgrammingResourcesApiClient;
public class ExampleEndpoint : EndPoint, IExampleEndpoint
{
	private readonly HttpClient _client;

	public ExampleEndpoint(HttpClient client)
	{
		_client = client;
	}

	public async Task<IEnumerable<Example>> Get(int resourceId)
	{
		var examples = await _client.GetFromJsonAsync<IEnumerable<Example>>($"api/Example/{resourceId}");
		ThrowIfNull(examples);
		return examples!;
	}

	public async Task<Example> Post(Example example)
	{
		using var response = await _client.PostAsJsonAsync($"/api/Example", example);
		CheckResponse(response);
		var resourceResp = await response.Content.ReadFromJsonAsync<Example>();
		ThrowIfNull(resourceResp);
		return resourceResp!;
	}

	public async Task Put(Example example)
	{
		using var response = await _client.PutAsJsonAsync($"/api/Example/{example.ExampleId}", example);
		CheckResponse(response);
	}

	public async Task Delete(int exampleId)
	{
		using var response = await _client.DeleteAsync($"/api/Example/{exampleId}");
		CheckResponse(response);
	}
}
