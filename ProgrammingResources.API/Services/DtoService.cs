using Mapster;
using ProgrammingResources.API.DTOs;
using ProgrammingResources.API.Services.Interfaces;
using ProgrammingResources.Library.Models;
using ProgrammingResources.Library.Services.Repos;
using System.Security.AccessControl;
using System.Security.Claims;
using Type = ProgrammingResources.Library.Models.Type;

namespace ProgrammingResources.API.Services;

public class DtoService : IDtoService
{
    private readonly IProgrammingLanguageRepo _languageRepo;
    private readonly ITypeRepo _typeRepo;
    private readonly ITagRepo _tagRepo;
    private readonly IExampleRepo _exampleRepo;

    public DtoService(IProgrammingLanguageRepo languageRepo,
        ITypeRepo typeRepo,
        ITagRepo tagRepo,
        IExampleRepo exampleRepo)
    {
        _languageRepo = languageRepo;
        _typeRepo = typeRepo;
        _tagRepo = tagRepo;
        _exampleRepo = exampleRepo;
    }

    /// <summary>
    /// Gets a ProgrammingLanguageDto from the provided ProgrammingLanguage.
    /// If the ProgrammingLanguage doesn't already exist it will be added.
    /// </summary>
    /// <param name="programmingLanguage"></param>
    /// <returns></returns>
    public async Task<ProgrammingLanguageDto?> GetProgrammingLangugeDto(ProgrammingLanguage? programmingLanguage)
    {
        if (programmingLanguage?.Language is not null)
        {
            var lang = await _languageRepo.Get(programmingLanguage.Language);
            if (lang is null)
            {
                var output = await _languageRepo.Add(programmingLanguage);
                return output.Adapt<ProgrammingLanguageDto>();
            }
            else
            {
                return lang.Adapt<ProgrammingLanguageDto>();
            }
        }

        return null;
    }

    /// <summary>
    /// Creates a TypeDto From the specified Type.
    /// If the type does not already exist it will be added.
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public async Task<TypeDto?> GetTypeDto(Type? type)
    {
        if (type is not null)
        {
            var outType = await _typeRepo.Get(type.Name);
            if (outType is null)
            {
                var newType = new Type
                {
                    Name = type.Name,
                    UserId = type.UserId,
                };
                await _typeRepo.Add(newType);
                return newType.Adapt<TypeDto>();
            }
            else
            {
                return outType.Adapt<TypeDto>();
            }
        }

        return null;
    }

    public async Task<ExampleDto?> GetExampleDto(Example? example, string? programmingLanguage)
    {
        if(example is not null)
        {
            if (programmingLanguage is not null)
            {
                var exampleLanguage = await _languageRepo.Get(programmingLanguage);
                if (exampleLanguage is not null)
                {
                    example.ProgrammingLanguageId = exampleLanguage.ProgrammingLanguageId;
                }
                else
                {
                    var newLang = new ProgrammingLanguage
                    {
                        Language = programmingLanguage,
                        UserId = example.UserId,
                    };
                    await _languageRepo.Add(newLang);

                    example.ProgrammingLanguageId = newLang.ProgrammingLanguageId;
                }
            }

            await _exampleRepo.Save(example);
            return example.Adapt<ExampleDto>();
        }
        return null;
    }

    public async Task<TagDto?> GetTagDto(Tag? tag)
    {
        if(tag is not null)
        {
            var outTag = await _tagRepo.Get(tag.Name);
            if (outTag is null)
            {
                var newTag = new Tag
                {
                    Name = tag.Name,
                    UserId = tag.UserId,
                };
                await _tagRepo.Add(newTag);
                return newTag.Adapt<TagDto>();
            }
            return outTag.Adapt<TagDto>();
        }
        return null;
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
