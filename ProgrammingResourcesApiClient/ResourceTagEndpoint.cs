using ProgrammingResourcesApiClient.Interfaces;
using ProgrammingResourcesApiClient.Models;
using System.Net.Http.Json;

namespace ProgrammingResourcesApiClient;
public class ResourceTagEndpoint :EndPoint, IResourceTagEndpoint
{
	private readonly HttpClient _client;

	public ResourceTagEndpoint(HttpClient client)
	{
		_client = client;
	}

	public async Task TagResource(int resourceId, string tag)
	{
		ResourceTagRequest request = new()
		{
			ResourceId = resourceId,
			TagName = tag
		};

        using var response = await _client.PostAsJsonAsync($"api/ResourceTag/TagResource", request);
		CheckResponse(response);
	}

	public async Task Delete(int resourceTagId)
	{
		var response = await _client.DeleteAsync($"api/ResourceTag/{resourceTagId}");
		CheckResponse(response);
	}

	public async Task<ResourceTag> Get(int resourceTagId)
	{
        var resourceTags = await _client.GetFromJsonAsync<ResourceTag>($"api/ResourceTag/Get/{resourceTagId}");
        ThrowIfNull(resourceTags);
        return resourceTags!;
    }

	public async Task<IEnumerable<ResourceTag>> GetAll()
	{
        var resourceTags = await _client.GetFromJsonAsync<IEnumerable<ResourceTag>>("api/ResourceTag/Get");
		ThrowIfNull(resourceTags);
		return resourceTags!;
    }

	public async Task<IEnumerable<Resource>> GetTaggedResources(int tagId)
	{
		var resources = await _client.GetFromJsonAsync<IEnumerable<Resource>>($"api/ResourceTag/Tagged/{tagId}");
		ThrowIfNull(resources);
		return resources!;
	}

	public async Task<ResourceTag> Post(ResourceTag resourceTag)
	{
		using var response = await _client.PostAsJsonAsync($"api/ResourceTag/", resourceTag);
		CheckResponse(response);
		var rtResponse = await response.Content.ReadFromJsonAsync<ResourceTag>();
		ThrowIfNull(rtResponse);

		return rtResponse!;
	}
}
