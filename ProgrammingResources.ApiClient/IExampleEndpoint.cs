using ProgrammingResources.ApiClient.Models;

namespace ProgrammingResources.ApiClient;
public interface IExampleEndpoint
{
    Task<Example> AddExample(Example example);
    Task<Example> Get(int exampleId);
    Task<IEnumerable<Example>> GetExamples(int resourceId);
}