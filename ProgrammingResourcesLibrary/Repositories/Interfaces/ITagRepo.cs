using ProgrammingResourcesLibrary.Models;

namespace ProgrammingResourcesLibrary.Repositories.Interfaces;
public interface ITagRepo
{
    Task<Tag?> Get(int tagId);
    Task<IEnumerable<Tag>> GetAll();
    Task Save(Tag tag);
}