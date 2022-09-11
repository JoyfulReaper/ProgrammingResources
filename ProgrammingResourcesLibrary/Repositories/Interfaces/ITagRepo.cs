using ProgrammingResourcesLibrary.Models;

namespace ProgrammingResourcesLibrary.Repositories.Interfaces;
public interface ITagRepo
{
    Task<Tag?> Get(int tagId);
    Task<IEnumerable<Tag>> GetAll();
    Task<IEnumerable<Tag>> GetByResourceId(int resourceId);
    Task Save(Tag tag);
}