using ProgrammingResources.ApiClient.Interface;
using ProgrammingResources.ApiClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingResources.ApiClient;

public class TagEndpoint : EndpointBase, ITagEndpoint
{
    private readonly HttpClient _client;

    public TagEndpoint(HttpClient client)
    {
        _client = client;
    }

    public async Task<Tag> Get(int tagId)
    {
        var tag = await _client.GetFromJsonAsync<Tag>($"api/v1/Tag/{tagId}");
        ThrowIfNull(tag);

        return tag!;
    }

    public async Task<IEnumerable<Tag>> GetAll()
    {
        var tags = await _client.GetFromJsonAsync<IEnumerable<Tag>>($"api/v1/Tag");
        ThrowIfNull(tags);

        return tags!;
    }

    public async Task Add(Tag type)
    {
        using var response = await _client.PutAsJsonAsync("api/v1/Tag", type);
        CheckResponse(response);
    }

    public async Task Delete(int tagId)
    {
        using var response = await _client.DeleteAsync($"api/v1/Tag/{tagId}");
        CheckResponse(response);
    }

    public async Task TagResource(int tagId, int resourceId)
    {
        using var response = await _client.PutAsJsonAsync($"/api/v1/Tag/tagResource", new
        {
            TagId = tagId,
            ResourceId = resourceId
        });

        CheckResponse(response);
    }
}
