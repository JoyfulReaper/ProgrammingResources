
using ProgrammingResourcesApiClient.Interfaces;
using ProgrammingResourcesApiClient.Models;
using System.Net.Http.Json;

namespace ProgrammingResourcesApiClient;

public class TagEndpoint : EndPoint, ITagEndpoint
{
	private readonly HttpClient _client;

	public TagEndpoint(HttpClient client)
	{
		_client = client;
	}

	public async Task<Tag> Get(int tagId)
	{
		var tag = await _client.GetFromJsonAsync<Tag>($"api/Tag/{tagId}");
		ThrowIfNull(tag);

		return tag!;
	}

	public async Task<IEnumerable<Tag>> GetAll()
	{
		var tags = await _client.GetFromJsonAsync<IEnumerable<Tag>>("api/Tag");
		ThrowIfNull(tags);
		return tags!;
	}

	public async Task<Tag> Post(Tag tag)
	{
		using var response = await _client.PostAsJsonAsync("api/Tag", tag);
		CheckResponse(response);
		var tagResp = await response.Content.ReadFromJsonAsync<Tag>();
		ThrowIfNull(tagResp);
		return tagResp!;
	}

    public async Task<List<Tag>> Post(List<Tag> tags)
    {
        using var response = await _client.PostAsJsonAsync("api/Tag/Multiple", tags);
        CheckResponse(response);
        var tagResp = await response.Content.ReadFromJsonAsync<List<Tag>>();
        ThrowIfNull(tagResp);
        return tagResp!;
    }
}
