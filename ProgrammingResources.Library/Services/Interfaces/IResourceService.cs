using ProgrammingResources.Library.Models;

namespace ProgrammingResources.Library.Services.Interfaces;
public interface IResourceService
{
    Task Delete(int resourceId);
    Task<Resource?> Get(int resourceId);
    Task<IEnumerable<Resource>> GetAll();
    Task<Resource> Save(Resource resource);
}