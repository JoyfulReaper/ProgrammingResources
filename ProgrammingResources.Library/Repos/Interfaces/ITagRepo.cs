using ProgrammingResources.Library.Models;

namespace ProgrammingResources.Library.Services.Repos;

public interface ITagRepo
{
    Task<Tag> Add(Tag tag);
    Task Delete(int tagId);
    Task<Tag?> Get(int tagId);
    Task<IEnumerable<Tag>> GetAll();
    Task TagResource(int tagId, int resourceId, string userId);
    Task UntagResource(int tagId, int resourceId);
}