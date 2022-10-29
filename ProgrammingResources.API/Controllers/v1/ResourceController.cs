using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProgrammingResources.API.DTOs;
using ProgrammingResources.Library.Services.Repos;

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
