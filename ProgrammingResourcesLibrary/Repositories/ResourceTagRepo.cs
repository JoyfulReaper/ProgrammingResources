using ProgrammingResourceLibrary.DataAccess;
using ProgrammingResourcesLibrary.Models;


namespace ProgrammingResourcesLibrary.Repositories;
public class ResourceTagRepo
{
	private readonly IDataAccess _dataAccess;

	public ResourceTagRepo(IDataAccess dataAccess)
	{
		_dataAccess = dataAccess;
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
        var id = await _dataAccess.SaveDataAndGetIdAsync("spResourceTag_Save", new
        {
            resourceTag.ResourceTagId,
            resourceTag.ResourceId,
            resourceTag.TagId
        }, "ProgrammingResources");
        
        resourceTag.ResourceTagId = id;
    }
}
