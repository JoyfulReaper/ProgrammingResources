using ProgrammingResources.Library.Models;

namespace ProgrammingResources.Library.Services.Repos;

public interface IProgrammingLanguageRepo
{
    Task<ProgrammingLanguage> Add(ProgrammingLanguage language);
    Task Delete(int programmingLanguageId);
    Task<ProgrammingLanguage?> Get(int ProgrammingLanguageId);
    Task<IEnumerable<ProgrammingLanguage>> GetAll();
}