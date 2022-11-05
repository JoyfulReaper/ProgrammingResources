namespace ProgrammingResources.ApiClient.Interface;

public interface ITypeEndpoint
{
    Task Add(string type);
    Task Delete(string type);
    Task<IEnumerable<Type>> GetAll();
}