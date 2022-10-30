using ProgrammingResources.API.DTOs;
using ProgrammingResources.Library.Models;

namespace ProgrammingResources.API.Services.Interfaces;
public interface IDtoService
{
    Task<ExampleDto?> GetExampleDto(Example? example, string? programmingLanguage);
    Task<ProgrammingLanguageDto?> GetProgrammingLangugeDto(ProgrammingLanguage? programmingLanguag);
    Task<ResourceDto> GetResourceDto(Resource resource);
    Task<TagDto?> GetTagDto(Tag? tag);
    Task<TypeDto?> GetTypeDto(Library.Models.Type? type);
}