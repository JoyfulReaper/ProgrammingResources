using ProgrammingResourceLibrary.DataAccess;
using ProgrammingResourcesLibrary.Models;
using ProgrammingResourcesLibrary.Repositories.Interfaces;

namespace ProgrammingResourcesLibrary.Repositories;
public class ResourceRepo : IResourceRepo
{
    private readonly IDataAccess _dataAccess;

    public ResourceRepo(IDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }

    public async Task<IEnumerable<Resource>> GetAll()
    {
        var output = await _dataAccess.LoadDataAsync<Resource, dynamic>("spResource_GetAll", new { }, "ProgrammingResources");
        return output;
    }

    public async Task<Resource?> Get(int resourceId)
    {
        var output = await _dataAccess.LoadDataAsync<Resource, dynamic>("spResource_Get", new { resourceId }, "ProgrammingResources");
        return output.SingleOrDefault();
    }

    public async Task Save(Resource resource)
    {
        var id = await _dataAccess.SaveDataAndGetIdAsync("spResource_Save", new
        {
            resource.ResourceId,
            resource.Title,
            resource.Description,
            resource.Url
        }, "ProgrammingResources");

        resource.ResourceId = id;
    }

    public async Task Delete(int resourceId)
    {
        await _dataAccess.SaveDataAsync<dynamic>("spResource_Delete", new { resourceId }, "ProgrammingResources");
    }
}
