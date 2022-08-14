using ProgrammingResourcesApiClient.Models;

namespace ProgrammingResourcesApiClient.Interfaces;
public interface IResourceEndpoint
{
    Task Delete(int resourceId);
    Task<Resource> Get(int resourceId);
    Task<IEnumerable<Resource>> GetAll();
    Task<Resource> Post(Resource resource);
    Task Put(Resource resource);
}