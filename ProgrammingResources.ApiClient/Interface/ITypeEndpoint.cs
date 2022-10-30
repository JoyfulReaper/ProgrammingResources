namespace ProgrammingResources.ApiClient.Interface;

public interface ITypeEndpoint
{
    Task Add(Type type);
    Task Delete(int typeId);
    Task<Type> Get(int typeId);
    Task<IEnumerable<Type>> GetAll();
}