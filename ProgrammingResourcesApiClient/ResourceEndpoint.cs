using ProgrammingResourcesApiClient.Interfaces;
using ProgrammingResourcesApiClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingResourcesApiClient;
public class ResourceEndpoint : EndPoint, IResourceEndpoint
{
	private readonly HttpClient _client;

	public ResourceEndpoint(HttpClient client)
	{
		_client = client;
	}

	public async Task<IEnumerable<Resource>> GetAll()
	{
		var resources = await _client.GetFromJsonAsync<IEnumerable<Resource>>("api/Resource");
		ThrowIfNull(resources);
		return resources!;
	}


	public async Task<Resource> Get(int resourceId)
	{
		var resource = await _client.GetFromJsonAsync<Resource>($"api/Resource/{resourceId}");
		ThrowIfNull(resource);
		return resource!;
	}

	public async Task<Resource> Post(Resource resource)
	{
		using var response = await _client.PostAsJsonAsync("api/Resource", resource);
		CheckResponse(response);
		var resourceResp = await response.Content.ReadFromJsonAsync<Resource>();
		ThrowIfNull(resourceResp);
		return resourceResp!;
	}

	public async Task Put(Resource resource)
	{
		using var response = await _client.PutAsJsonAsync($"api/Resource/{resource.ResourceId}", resource);
		CheckResponse(response);
	}

	public async Task Delete(int resourceId)
	{
		using var response = await _client.DeleteAsync($"api/Resource/{resourceId}");
		CheckResponse(response);
	}
}
