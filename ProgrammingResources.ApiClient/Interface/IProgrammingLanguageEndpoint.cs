using ProgrammingResources.ApiClient.Models;

namespace ProgrammingResources.ApiClient.Interface;
public interface IProgrammingLanguageEndpoint
{
    Task Add(ProgrammingLanguage programmingLanguage);
    Task Delete(int programmingLangaugeId);
    Task<ProgrammingLanguage> Get(int programmingLanguageId);
    Task<IEnumerable<ProgrammingLanguage>> GetAll();
}