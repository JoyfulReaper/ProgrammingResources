using Mapster;
using ProgrammingResources.API.DTOs;
using ProgrammingResources.API.Services.Interfaces;
using ProgrammingResources.Library.Models;
using ProgrammingResources.Library.Services.Repos;

namespace ProgrammingResources.API.Services;

public class ResourceService : IResourceService
{
    private readonly IProgrammingLanguageRepo _languageRepo;
    private readonly ITypeRepo _typeRepo;
    private readonly ITagRepo _tagRepo;
    private readonly IExampleRepo _exampleRepo;

    public ResourceService(IProgrammingLanguageRepo languageRepo,
        ITypeRepo typeRepo,
        ITagRepo tagRepo,
        IExampleRepo exampleRepo)
    {
        _languageRepo = languageRepo;
        _typeRepo = typeRepo;
        _tagRepo = tagRepo;
        _exampleRepo = exampleRepo;
    }

    public async Task<ResourceDto> GetResourceDto(Resource resource)
    {
        var resourceDto = resource.Adapt<ResourceDto>();

        // Programming Language
        if (resource.ProgrammingLanguageId is not null)
        {
            resourceDto.ProgramingLanguage = (await _languageRepo.Get(resource.ProgrammingLanguageId.Value))?.Adapt<ProgrammingLanguageDto>();
        }

        // Type
        if (resource.TypeId is not null)
        {
            resourceDto.Type = (await _typeRepo.Get(resource.TypeId.Value))?.Adapt<TypeDto>();
        }

        var examples = await _exampleRepo.GetAll(resource.ResourceId);
        var tags = await _tagRepo.GetByResource(resource.ResourceId);

        // Examples
        foreach (var example in examples)
        {
            resourceDto.Examples.Add(example.Adapt<ExampleDto>());
        }

        // Tags
        foreach (var tag in tags)
        {
            resourceDto.Tags.Add(tag.Adapt<TagDto>());
        }

        return resourceDto;
    }
}
