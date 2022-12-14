using ProgrammingResourceLibrary.DataAccess;
using ProgrammingResourcesLibrary.Models;
using ProgrammingResourcesLibrary.Repositories.Interfaces;

namespace ProgrammingResourcesLibrary.Repositories;
public class ResourceTagRepo : IResourceTagRepo
{
    private readonly IDataAccess _dataAccess;

    public ResourceTagRepo(IDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }

    public async Task<IEnumerable<Resource>> GetTagged(int tagId)
    {
        var output = await _dataAccess.LoadDataAsync<Resource, dynamic>("spResource_GetResourceByTag", new { tagId }, "ProgrammingResources");
        return output;
    }

    public async Task<IEnumerable<ResourceTag>> GetAll()
    {
        var output = await _dataAccess.LoadDataAsync<ResourceTag, dynamic>("spResourceTag_GetAll", new { }, "ProgrammingResources");
        return output;
    }

    public async Task<ResourceTag?> Get(int resourceTagId)
    {
        var output = await _dataAccess.LoadDataAsync<ResourceTag, dynamic>("spResourceTag_Get", new { resourceTagId }, "ProgrammingResources");
        return output.SingleOrDefault();
    }

    public async Task Save(ResourceTag resourceTag)
    {
        await _dataAccess.SaveDataAsync("spResourceTag_Save", new
        {
            resourceTag.ResourceId,
            resourceTag.TagId
        }, "ProgrammingResources");
    }

    public async Task Delete(int resourceTagId)
    {
        await _dataAccess.ExecuteRawSql("DELETE FROM ResourceTag WHERE ResourceTagId = @ResourceTagId", new { resourceTagId }, "ProgrammingResouces");
    }
}
