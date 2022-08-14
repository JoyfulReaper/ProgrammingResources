using ProgrammingResourcesLibrary.Models;

namespace ProgrammingResourcesLibrary.Repositories.Interfaces;
public interface IResourceTagRepo
{
    Task Delete(int resourceTagId);
    Task<ResourceTag?> Get(int resourceTagId);
    Task<IEnumerable<ResourceTag>> GetAll();
    Task<IEnumerable<Resource>> GetTagged(int tagId);
    Task Save(ResourceTag resourceTag);
}