using ProgrammingResources.ApiClient.Models;

namespace ProgrammingResources.ApiClient.Interface;
public interface IExampleEndpoint
{
    Task<Example> Add(Example example);
    Task Delete(int exampleId);
    Task<Example> Get(int exampleId);
    Task<Example> GetByResource(int resourceId);
    Task Update(Example example);
}