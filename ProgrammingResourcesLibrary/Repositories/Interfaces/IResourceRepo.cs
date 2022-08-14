using ProgrammingResourcesLibrary.Models;

namespace ProgrammingResourcesLibrary.Repositories.Interfaces;
public interface IResourceRepo
{
    Task Delete(int resourceId);
    Task<Resource?> Get(int resourceId);
    Task<IEnumerable<Resource>> GetAll();
    Task Save(Resource resource);
}