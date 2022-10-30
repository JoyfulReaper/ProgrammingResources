using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProgrammingResources.API.DTOs;
using ProgrammingResources.API.Services.Interfaces;
using ProgrammingResources.Library.Models;
using ProgrammingResources.Library.Services.Repos;
using System.Security.Claims;
using Type = ProgrammingResources.Library.Models.Type;

namespace ProgrammingResources.API.Controllers.v1;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiController]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
public class ResourceController : ControllerBase
{
    private readonly IResourceService _resourceService;
    private readonly IResourceRepo _resourceRepo;
    private readonly IProgrammingLanguageRepo _languageRepo;
    private readonly ITypeRepo _typeRepo;
    private readonly IExampleRepo _exampleRepo;
    private readonly ITagRepo _tagRepo;
    private readonly ILogger<ResourceController> _logger;

    public ResourceController(IResourceService resourceService,
        IResourceRepo resourceRepo,
        IProgrammingLanguageRepo languageRepo,
        ITypeRepo typeRepo,
        IExampleRepo exampleRepo,
        ITagRepo tagRepo,
        ILogger<ResourceController> logger)
    {
        _resourceService = resourceService;
        _resourceRepo = resourceRepo;
        _languageRepo = languageRepo;
        _typeRepo = typeRepo;
        _exampleRepo = exampleRepo;
        _tagRepo = tagRepo;
        _logger = logger;
    }

    [HttpPost(Name = "ResourceAdd")]
    public async Task<ActionResult<ResourceDto>> AddResource([FromBody] CreateResourceRequest resourceRequest)
    {
        // TODO: Clean this up, make a service and break into parts
        // TODO: Should do this whole thing in a transaction
        try
        {
            var resource = resourceRequest.Adapt<Resource>();

            // Programming Language
            ProgrammingLanguageDto? programmingLanguage = default;
            if (resourceRequest.Langauge is not null)
            {
                var lang = await _languageRepo.Get(resourceRequest.Langauge);
                if (lang is null)
                {
                    var newLang = new ProgrammingLanguage
                    {
                        Language = resourceRequest.Langauge,
                        UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value,
                    };
                    await _languageRepo.Add(newLang);
                    resource.ProgrammingLanguageId = newLang.ProgrammingLanguageId;
                    programmingLanguage = newLang.Adapt<ProgrammingLanguageDto>();
                }
                else
                {
                    resource.ProgrammingLanguageId = lang.ProgrammingLanguageId;
                    programmingLanguage = new ProgrammingLanguageDto() { Language = resourceRequest.Langauge, ProgrammingLanguageId = lang.ProgrammingLanguageId };
                }
            }

            // Type
            TypeDto? resourceType = default;
            if (resourceRequest.Type is not null)
            {
                var type = await _typeRepo.Get(resourceRequest.Type);
                if (type is null)
                {
                    var newType = new Type
                    {
                        Name = resourceRequest.Type,
                        UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value,
                    };
                    await _typeRepo.Add(newType);
                    resource.TypeId = newType.TypeId;
                    resourceType = newType.Adapt<TypeDto>();
                }
                else
                {
                    resource.TypeId = type.TypeId;
                    resourceType = new TypeDto() { Name = resourceRequest.Type, TypeId = type.TypeId };
                }
            }

            // Resource
            resource.UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
            var output = await _resourceRepo.Save(resource);
            var outputDto = output.Adapt<ResourceDto>();
            outputDto.ProgramingLanguage = programmingLanguage;
            outputDto.Type = resourceType;


            // Tags
            foreach (var tagRequest in resourceRequest.Tags)
            {
                var tag = await _tagRepo.Get(tagRequest);
                if (tag is null)
                {
                    var newTag = new Tag
                    {
                        Name = tagRequest,
                        UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value,
                    };
                    await _tagRepo.Add(newTag);
                    tag = newTag;
                }
                await _tagRepo.TagResource(tag.TagId, output.ResourceId, User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value);
                outputDto.Tags.Add(tag.Adapt<TagDto>());
            }

            // Examples
            foreach (var exampleRequest in resourceRequest.Examples)
            {
                var example = exampleRequest.Adapt<Example>();
                example.ResourceId = output.ResourceId;

                if (exampleRequest.Language is not null)
                {
                    var exampleLanguage = await _languageRepo.Get(exampleRequest.Language);
                    if (exampleLanguage is not null)
                    {
                        example.ProgrammingLanguageId = exampleLanguage.ProgrammingLanguageId;
                    }
                    else
                    {
                        var newLang = new ProgrammingLanguage
                        {
                            Language = resourceRequest.Langauge,
                            UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value,
                        };
                        await _languageRepo.Add(newLang);
                        resource.ProgrammingLanguageId = newLang.ProgrammingLanguageId;

                        example.ProgrammingLanguageId = newLang.ProgrammingLanguageId;
                    }
                }

                example.UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
                await _exampleRepo.Save(example);
                outputDto.Examples.Add(example.Adapt<ExampleDto>());
            }

            return CreatedAtAction(nameof(Get), new { resourceId = output.ResourceId }, outputDto);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "AddResource() Failed");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("{resourceId}", Name = "ResourceGet")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResourceDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Resource>> Get(int resourceId)
    {
        try
        {
            var resource = await _resourceRepo.Get(resourceId);
            if(resource is null)
            {
                return NotFound();
            }

            return Ok(await _resourceService.GetResourceDto(resource));
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Get() Failed");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet(Name = "ResourceGetAll")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ResourceDto>))]
    public async Task<ActionResult<IEnumerable<ResourceDto>>> GetAllResources()
    {
        try
        {
            var output = new List<ResourceDto>();
            var allResources = await _resourceRepo.GetAll();

            foreach (var resource in allResources)
            {
                var rDto = await _resourceService.GetResourceDto(resource);
                output.Add(rDto);
            }

            return Ok(output);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "GetAllResources() Failed");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
