using ProgrammingResources.Library.Models;

namespace ProgrammingResources.Library.Services.Repos;

public interface IProgrammingLanguageRepo
{
    Task<ProgrammingLanguage> Add(ProgrammingLanguage language);
    Task Delete(int programmingLanguageId);
    Task<ProgrammingLanguage?> Get(int ProgrammingLanguageId);
    Task<ProgrammingLanguage?> Get(string language);
    Task<IEnumerable<ProgrammingLanguage>> GetAll();
}