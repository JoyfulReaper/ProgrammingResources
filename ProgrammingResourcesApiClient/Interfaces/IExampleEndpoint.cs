using ProgrammingResourcesApiClient.Models;

namespace ProgrammingResourcesApiClient.Interfaces;
public interface IExampleEndpoint
{
    Task Delete(int exampleId);
    Task<IEnumerable<Example>> Get(int resourceId);
    Task<Example> Post(Example example);
    Task Put(Example example);
}