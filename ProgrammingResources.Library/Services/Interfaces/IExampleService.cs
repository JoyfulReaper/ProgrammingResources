using ProgrammingResources.Library.Models;

namespace ProgrammingResources.Library.Services.Interfaces;
public interface IExampleService
{
    Task Delete(int exampleId);
    Task<Example?> Get(int exampleId);
    Task<IEnumerable<Example>> GetAll(int resrouceId);
    Task<Example> Save(Example example);
}