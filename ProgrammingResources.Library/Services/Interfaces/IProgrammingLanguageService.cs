using ProgrammingResources.Library.Models;

namespace ProgrammingResources.Library.Services.Interfaces;
public interface IProgrammingLanguageService
{
    Task<ProgrammingLanguage> Add(ProgrammingLanguage language);
    Task Delete(int programmingLanguageId);
    Task<ProgrammingLanguage?> Get(int ProgrammingLanguageId);
    Task<IEnumerable<ProgrammingLanguage>> GetAll();
}