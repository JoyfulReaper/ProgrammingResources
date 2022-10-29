using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProgrammingResources.API.DTOs;
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
    private readonly IResourceRepo _resourceRepo;
    private readonly IProgrammingLanguageRepo _languageRepo;
    private readonly ITypeRepo _typeRepo;
    private readonly IExampleRepo _exampleRepo;
    private readonly ITagRepo _tagRepo;
    private readonly ILogger<ResourceController> _logger;

    public ResourceController(IResourceRepo resourceService,
        IProgrammingLanguageRepo languageRepo,
        ITypeRepo typeRepo,
        IExampleRepo exampleRepo,
        ITagRepo tagRepo,
        ILogger<ResourceController> logger)
    {
        _resourceRepo = resourceService;
        _languageRepo = languageRepo;
        _typeRepo = typeRepo;
        _exampleRepo = exampleRepo;
        _tagRepo = tagRepo;
        _logger = logger;
    }

    [HttpPost(Name = "ResourceAdd")]
    public async Task<ActionResult<ResourceDto>> AddResource([FromBody]CreateResourceRequest resourceRequest)
    {
        try
        {
            var resource = resourceRequest.Adapt<Resource>();

            // Programming Language
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
                }
                else
                {
                    resource.ProgrammingLanguageId = lang.ProgrammingLanguageId;
                }
            }

            // Type
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
                }
                else
                {
                    resource.TypeId = type.TypeId;
                }
            }

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
                }
            }

            var output = await _resourceRepo.Save(resource);
            var outputDto = output.Adapt<ResourceDto>();

            // Examples
            foreach (var exampleRequest in resourceRequest.Examples)
            {
                var example = exampleRequest.Adapt<Example>();
                example.ResourceId = output.ResourceId;

                await _exampleRepo.Save(example);
                outputDto.Examples.Add(example.Adapt<ExampleDto>());
            }

            return CreatedAtAction(nameof(Get), new { resouorceId = output.ResourceId }, outputDto);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "AddResource() Failed");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("{resourceId}", Name = "ResourceGet")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResourceDto))]
    public async Task<Resource> Get(int resourceId)
    {
        throw new NotImplementedException();
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
                var rDto = resource.Adapt<ResourceDto>();
                if (resource.ProgrammingLanguageId is not null)
                {
                    rDto.ProgramingLanguage = (await _languageRepo.Get(resource.ProgrammingLanguageId.Value))?.Adapt<ProgrammingLanguageDto>();
                }
                if (resource.TypeId is not null)
                {
                    rDto.Type = (await _typeRepo.Get(resource.TypeId.Value))?.Adapt<TypeDto>();
                }

                var examples = await _exampleRepo.GetAll(resource.ResourceId);
                var tags = await _tagRepo.GetByResource(resource.ResourceId);

                foreach (var example in examples)
                {
                    rDto.Examples.Add(example.Adapt<ExampleDto>());
                }
                foreach (var tag in tags)
                {
                    rDto.Tags.Add(tag.Adapt<TagDto>());
                }
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
