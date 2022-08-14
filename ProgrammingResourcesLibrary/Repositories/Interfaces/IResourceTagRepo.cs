using ProgrammingResourcesLibrary.Models;

namespace ProgrammingResourcesLibrary.Repositories.Interfaces;
public interface IResourceTagRepo
{
    Task<ResourceTag?> Get(int resourceTagId);
    Task<IEnumerable<ResourceTag>> GetAll();
    Task Save(ResourceTag resourceTag);
}