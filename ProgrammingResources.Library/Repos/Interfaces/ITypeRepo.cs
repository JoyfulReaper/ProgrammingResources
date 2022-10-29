namespace ProgrammingResources.Library.Services.Repos;

public interface ITypeRepo
{
    Task<Models.Type> Add(Models.Type type);
    Task Delete(int typeId);
    Task<Models.Type?> Get(int typeId);
    Task<Models.Type?> Get(string type);
    Task<IEnumerable<Models.Type>> GetAll();
}