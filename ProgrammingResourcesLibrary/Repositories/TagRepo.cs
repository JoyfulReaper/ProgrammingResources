using ProgrammingResourceLibrary.DataAccess;
using ProgrammingResourcesLibrary.Models;

namespace ProgrammingResourcesLibrary.Repositories;
public class TagRepo
{
	private readonly IDataAccess _dataAccess;

	public TagRepo(IDataAccess dataAccess)
	{
		_dataAccess = dataAccess;
	}

    public async Task<IEnumerable<Tag>> GetAll()
    {
        var output = await _dataAccess.LoadDataAsync<Tag, dynamic>("spTag_GetAll", new { }, "ProgrammingResources");
        return output;
    }

    public async Task<Tag?> Get(int tagId)
    {
        var output = await _dataAccess.LoadDataAsync<Tag, dynamic>("spTag_Get", new { tagId }, "ProgrammingResources");
        return output.SingleOrDefault();
    }

    public async Task Save(Tag tag)
    {
        var id = await _dataAccess.SaveDataAndGetIdAsync("spTag_Save", new
        {
            tag.TagId,
            tag.Name
        }, "ProgrammingResources");
        tag.TagId = id;
    }
        
}
