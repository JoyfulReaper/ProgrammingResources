using ProgrammingResourceLibrary.DataAccess;
using ProgrammingResourcesLibrary.Models;
using ProgrammingResourcesLibrary.Repositories.Interfaces;

namespace ProgrammingResourcesLibrary.Repositories;
public class ExampleRepo : IExampleRepo
{
    private readonly IDataAccess _dataAccess;

    public ExampleRepo(IDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }

    public async Task<IEnumerable<Example>> Get(int resourceId)
    {
        return await _dataAccess.LoadDataAsync<Example, dynamic>("spExample_Get", new { resourceId }, "ProgrammingResources");
    }

    public async Task Save(Example example)
    {
        var id = await _dataAccess.SaveDataAndGetIdAsync("spExample_Save", new
        {
            example.ResourceId,
            example.ExampleId,
            example.ExampleText,
        }, "ProgrammingResource");

        example.ExampleId = id;
    }

    public async Task Delete(int exampleId)
    {
        await _dataAccess.SaveDataAsync("spExample_Delete", new { exampleId }, "ProgrammingResources");
    }
}
