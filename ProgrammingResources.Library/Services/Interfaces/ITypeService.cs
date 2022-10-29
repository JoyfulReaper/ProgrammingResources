namespace ProgrammingResources.Library.Services.Interfaces;

public interface ITypeService
{
    Task<Models.Type> Add(Models.Type type);
    Task Delete(int typeId);
    Task<Models.Type?> Get(int typeId);
    Task<IEnumerable<Models.Type>> GetAll();
}