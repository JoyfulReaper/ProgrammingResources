using Mapster;
using ProgrammingResources.API.DTOs;
using ProgrammingResources.Library.Models;
using ProgrammingResources.Library.Services;
using ProgrammingResources.Library.Services.Repos;
using Type = ProgrammingResources.Library.Models.Type;

namespace ProgrammingResources.API.Services;

public class DtoService : IDtoService
{
    private readonly IProgrammingLanguageRepo _languageRepo;
    private readonly IResourceRepo _resourceRepo;
    private readonly ITypeRepo _typeRepo;
    private readonly ITagRepo _tagRepo;
    private readonly IExampleRepo _exampleRepo;

    public DtoService(IProgrammingLanguageRepo languageRepo,
        IResourceRepo resourceRepo,
        ITypeRepo typeRepo,
        ITagRepo tagRepo,
        IExampleRepo exampleRepo)
    {
        _languageRepo = languageRepo;
        _resourceRepo = resourceRepo;
        _typeRepo = typeRepo;
        _tagRepo = tagRepo;
        _exampleRepo = exampleRepo;
    }

    public async Task<ResourceDto?> GetResouorceDto(int resourceId)
    {
        var resource = await _resourceRepo.Get(resourceId);
        if (resource is null)
        {
            return null;
        }

        var rDto = resource.Adapt<ResourceDto>();

        if (resource.ProgrammingLanguageId is not null)
        {
            var language = await _languageRepo.Get(resource.ProgrammingLanguageId.Value);
            rDto.Langauge = language?.Language;
        }

        if (resource.TypeId is not null)
        {
            var type = await _typeRepo.Get(resource.TypeId.Value);
            rDto.Type = type?.Name;
        }

        var tags = (await _tagRepo.GetByResource(resourceId)).ToList();
        tags.ForEach(t => rDto.Tags.Add(t.Name));

        var examples = (await _exampleRepo.GetAll(resourceId)).ToList();
        foreach (var example in examples)
        {
            var eDto = example.Adapt<ExampleDto>();
            if (example.ProgrammingLanguageId is not null)
            {
                var language = await _languageRepo.Get(example.ProgrammingLanguageId.Value);
                eDto.Language = language?.Language;
            }

            if (example.TypeId is not null)
            {
                var type = await _typeRepo.Get(example.TypeId.Value);
                eDto.Type = type?.Name;
            }
            rDto.Examples.Add(eDto);
        }
        return rDto;
    }

    public async Task<ProgrammingLanguage> GetOrAddLanguage(string langauge, string userId)
    {
        var plDb = await _languageRepo.Get(langauge);
        if (plDb is null)
        {
            plDb = await _languageRepo.Add(new ProgrammingLanguage
            {
                Language = langauge,
                UserId = userId,
            });
        }

        return plDb;
    }

    public async Task<Type> GetOrAddType(string type, string userId)
    {
        var typeDb = await _typeRepo.Get(type);
        if(typeDb is null)
        {
            typeDb = await _typeRepo.Add(new Type
            {
                Name = type,
                UserId = userId,
            });
        }

        return typeDb;
    }

    public async Task<Tag> GetOrAddTag(string tag, string userId)
    {
        var tagDb = await _tagRepo.Get(tag);
        if(tagDb is null)
        {
            tagDb = await _tagRepo.Add(new Tag
            {
                Name = tag,
                UserId = userId,
            });
        }

        return tagDb;
    }

    public async Task AddLangugeAndType(Example example, ExampleDto exampleDto)
    {
        if (example.ProgrammingLanguageId.HasValue)
        {
            exampleDto.Language = (await _languageRepo.Get(example.ProgrammingLanguageId.Value))?.Language;
        }

        if (example.TypeId.HasValue)
        {
            exampleDto.Type = (await _typeRepo.Get(example.TypeId.Value))?.Name;
        }
    }
}