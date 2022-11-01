using ProgrammingResources.ApiClient.Models;

namespace ProgrammingResources.ApiClient.Interface;
public interface ITagEndpoint
{
    Task Add(string tag);
    Task Delete(string tag);
    Task<IEnumerable<string>> GetAll();
    Task TagResource(int tagId, int resourceId);
}