using ProgrammingResources.ApiClient.Models;

namespace ProgrammingResources.ApiClient.Interface;
public interface IResourceEndpoint
{
    Task<Resource> Add(Resource resource);
    Task<Resource> Get(int resourceId);
    Task<IEnumerable<Resource>> GetAll();
}