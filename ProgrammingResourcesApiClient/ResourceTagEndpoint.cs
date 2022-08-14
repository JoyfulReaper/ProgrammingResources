using ProgrammingResourcesApiClient.Interfaces;
using ProgrammingResourcesApiClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingResourcesApiClient;
public class ResourceTagEndpoint :EndPoint, IResourceTagEndpoint
{
	private readonly HttpClient _client;

	public ResourceTagEndpoint(HttpClient client)
	{
		_client = client;
	}

	public Task Delete(int resourceTagId)
	{
		throw new NotImplementedException();
	}

	public Task<ResourceTag> Get(int resourceTagId)
	{
		throw new NotImplementedException();
	}

	public Task<IEnumerable<ResourceTag>> GetAll()
	{
		throw new NotImplementedException();
	}

	public Task<IEnumerable<Resource>> GetTaggedResources(int tagId)
	{
		throw new NotImplementedException();
	}

	public Task<Resource> Post(ResourceTag resourceTag)
	{
		throw new NotImplementedException();
	}
}
