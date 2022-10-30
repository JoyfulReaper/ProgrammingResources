using ProgrammingResources.ApiClient.Models;

namespace ProgrammingResources.ApiClient.Interface;
public interface ITagEndpoint
{
    Task Add(Tag type);
    Task Delete(int tagId);
    Task<Tag> Get(int tagId);
    Task<IEnumerable<Tag>> GetAll();
    Task TagResource(int tagId, int resourceId);
}