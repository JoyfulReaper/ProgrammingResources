using ProgrammingResources.Library.Models;

namespace ProgrammingResources.Library.Services.Repos;

public interface IResourceRepo
{
    Task Delete(int resourceId);
    Task<Resource?> Get(int resourceId);
    Task<IEnumerable<Resource>> GetAll();
    Task<Resource> Save(Resource resource);
}