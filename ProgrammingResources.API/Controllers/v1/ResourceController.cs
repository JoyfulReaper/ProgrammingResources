using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProgrammingResources.API.DTOs;
using ProgrammingResources.API.Services;
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
    private readonly IProgrammingLanguageRepo _languageRepo;
    private readonly ITypeRepo _typeRepo;
    private readonly IExampleRepo _exampleRepo;
    private readonly ILogger<ResourceController> _logger;

    public ResourceController(IDtoService dtoService,
        IResourceRepo resourceRepo,
        ITagRepo tagRepo,
        IProgrammingLanguageRepo languageRepo,
        ITypeRepo typeRepo,
        IExampleRepo exampleRepo,
        ILogger<ResourceController> logger)
    {
        _dtoService = dtoService;
        _resourceRepo = resourceRepo;
        _tagRepo = tagRepo;
        _languageRepo = languageRepo;
        _typeRepo = typeRepo;
        _exampleRepo = exampleRepo;
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

            var output = resource.Adapt<ResourceDto>();

            if(resourceRequest.Langauge is not null)
            {
                var pl = await _dtoService.GetOrAddLanguage(resourceRequest.Langauge, userId);
                resource.ProgrammingLanguageId = pl.ProgrammingLanguageId;
                output.Langauge = pl.Language;
            }

            if(resourceRequest.Type is not null)
            {
                var type = await _dtoService.GetOrAddType(resourceRequest.Type, userId);
                resource.TypeId = type.TypeId;
                output.Type = type.Name;
            }

            resource.UserId = userId;
            await _resourceRepo.Save(resource);
            output.ResourceId = resource.ResourceId;

            foreach (var example in resourceRequest.Examples)
            {
                var newExample = new Example
                {
                    UserId = userId,
                    ResourceId = resource.ResourceId,
                    Text = example.Text,
                    Url = example.Url,
                    Page = example.Page,
                    TypeId = example.Type == null ? null : (await _dtoService.GetOrAddType(example.Type, userId)).TypeId,
                    ProgrammingLanguageId = example.Language == null ? null : (await _dtoService.GetOrAddLanguage(example.Language, userId)).ProgrammingLanguageId,
                };

                await _exampleRepo.Save(newExample);

                var outExample = newExample.Adapt<ExampleDto>();
                outExample.Language = example.Language;
                output.Examples.Add(outExample);
            }

            foreach (var tag in resourceRequest.Tags)
            {
                var tagDb = await _dtoService.GetOrAddTag(tag, userId);
                await _tagRepo.TagResource(resource.ResourceId, tagDb.TagId, userId);
                output.Tags.Add(tagDb.Name);
            }

            return CreatedAtAction(nameof(Get), new { resourceId = resource.ResourceId }, output);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, $"{nameof(AddResource)}() Failed");
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

            throw new NotImplementedException();

        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, $"{nameof(Get)}() Failed");
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


            return Ok(output);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, $"{nameof(GetAllResources)}() Failed");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
