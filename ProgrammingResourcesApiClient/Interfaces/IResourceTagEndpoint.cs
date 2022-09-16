

using ProgrammingResourcesApiClient.Models;

namespace ProgrammingResourcesApiClient.Interfaces;
public interface IResourceTagEndpoint
{
    Task<IEnumerable<Resource>> GetTaggedResources(int tagId);
    Task Delete(int resourceTagId);
    Task<ResourceTag> Get(int resourceTagId);
    Task<IEnumerable<ResourceTag>> GetAll();
    Task<ResourceTag> Post(ResourceTag resourceTag);
}