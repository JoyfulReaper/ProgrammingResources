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
    private readonly IDtoService _dtoService;
    private readonly IResourceRepo _resourceRepo;
    private readonly ITagRepo _tagRepo;
    private readonly ILogger<ResourceController> _logger;

    public ResourceController(IDtoService dtoService,
        IResourceRepo resourceRepo,
        ITagRepo tagRepo,
        ILogger<ResourceController> logger)
    {
        _dtoService = dtoService;
        _resourceRepo = resourceRepo;
        _tagRepo = tagRepo;
        _logger = logger;
    }

    [HttpPost(Name = "ResourceAdd")]
    public async Task<ActionResult<ResourceDto>> AddResource([FromBody] CreateResourceRequest resourceRequest)
    {
        // TODO: Clean this up
        // TODO: Should do this whole thing in a transaction
        try
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
            var resource = resourceRequest.Adapt<Resource>();

            // Programming Language
            ProgrammingLanguageDto? programmingLanguage = default;
            if (resourceRequest.Langauge is not null)
            {
                programmingLanguage = await _dtoService.GetProgrammingLangugeDto(new ProgrammingLanguage
                {
                    Language = resourceRequest.Langauge,
                    UserId = userId
                });
            }

            // Type
            TypeDto? resourceType = default;
            if (resourceRequest.Type is not null)
            {
                resourceType = await _dtoService.GetTypeDto(new Type
                {
                    Name = resourceRequest.Type,
                    UserId = userId
                });
            }

            // Resource
            resource.UserId = userId;
            var output = await _resourceRepo.Save(resource);
            var outputDto = output.Adapt<ResourceDto>();

            outputDto.ProgramingLanguage = programmingLanguage;
            outputDto.Type = resourceType;

            // Tags
            foreach (var tagRequest in resourceRequest.Tags)
            {
                var tag = await _dtoService.GetTagDto(new Tag
                {
                    Name = tagRequest,
                    UserId = userId
                });

                await _tagRepo.TagResource(tag.TagId, output.ResourceId, userId);
                outputDto.Tags.Add(tag.Adapt<TagDto>());
            }

            // Examples
            foreach (var exampleRequest in resourceRequest.Examples)
            {
                var example = exampleRequest.Adapt<Example>();
                example.ResourceId = output.ResourceId;
                example.UserId = userId;

                var exampleDto = await _dtoService.GetExampleDto(example, exampleRequest.Language);
                outputDto.Examples.Add(exampleDto);
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
            if (resource is null)
            {
                return NotFound();
            }

            return Ok(await _dtoService.GetResourceDto(resource));
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
                var rDto = await _dtoService.GetResourceDto(resource);
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
