namespace ProgrammingResources.Library.Services.Repos;

public interface ITypeRepo
{
    Task<Models.Type> Add(Models.Type type);
    Task Delete(int typeId);
    Task<Models.Type?> Get(int typeId);
    Task<IEnumerable<Models.Type>> GetAll();
}