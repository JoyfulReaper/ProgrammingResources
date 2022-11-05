using ProgrammingResources.ApiClient.Models;

namespace ProgrammingResources.ApiClient.Interface;
public interface IProgrammingLanguageEndpoint
{
    Task Add(string programmingLanguage);
    Task Delete(string programmingLangaugeId);
    Task<IEnumerable<string>> GetAll();
}