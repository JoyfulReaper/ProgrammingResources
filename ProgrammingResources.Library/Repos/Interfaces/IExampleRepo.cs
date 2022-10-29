using ProgrammingResources.Library.Models;

namespace ProgrammingResources.Library.Services.Repos;

public interface IExampleRepo
{
    Task Delete(int exampleId);
    Task<Example?> Get(int exampleId);
    Task<IEnumerable<Example>> GetAll(int resrouceId);
    Task<Example> Save(Example example);
}