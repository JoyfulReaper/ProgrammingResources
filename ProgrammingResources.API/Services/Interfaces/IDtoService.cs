using ProgrammingResources.API.DTOs;
using ProgrammingResources.Library.Models;

namespace ProgrammingResources.API.Services;
public interface IDtoService
{
    Task AddLangugeAndType(Example example, ExampleDto exampleDto);
    Task<ProgrammingLanguage> GetOrAddLanguage(string langauge, string userId);
    Task<Tag> GetOrAddTag(string tag, string userId);
    Task<Library.Models.Type> GetOrAddType(string type, string userId);
    Task<ResourceDto?> GetResouorceDto(int resourceId);
}