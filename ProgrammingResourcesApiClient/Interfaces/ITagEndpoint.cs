using ProgrammingResourcesApiClient.Models;

namespace ProgrammingResourcesApiClient.Interfaces;
public interface ITagEndpoint
{
    Task<Tag> Get(int tagId);
    Task<IEnumerable<Tag>> GetAll();
    Task<Tag> Post(Tag tag);
}