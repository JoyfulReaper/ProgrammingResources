using Mapster;
using Microsoft.AspNetCore.Mvc;
using ProgrammingResources.API.DTOs;
using ProgrammingResources.API.Services;
using ProgrammingResources.Library.Models;
using ProgrammingResources.Library.Services.Repos;
using System.Security.Claims;

namespace ProgrammingResources.API.Controllers.v1;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiController]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
public class ExampleController : ControllerBase
{
    private readonly IExampleRepo _exampleRepo;
    private readonly IDtoService _dtoService;
    private readonly ITypeRepo _typeRepo;
    private readonly IProgrammingLanguageRepo _languageRepo;
    private readonly ILogger<ExampleController> _logger;

    public ExampleController(IExampleRepo exampleRepo,
        IDtoService dtoService,
        ITypeRepo typeRepo,
        IProgrammingLanguageRepo languageRepo,
        ILogger<ExampleController> logger)
    {
        _exampleRepo = exampleRepo;
        _dtoService = dtoService;
        _typeRepo = typeRepo;
        _languageRepo = languageRepo;
        _logger = logger;
    }

    [HttpPut(Name = "ExampleUpdate")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateExample(ExampleDto example)
    {
        try
        {
            var exampleDb = await _exampleRepo.Get(example.ExampleId);
            if (exampleDb is null)
            {
                return NotFound();
            }

            exampleDb.Text = example.Text;
            exampleDb.Url = example.Url;
            exampleDb.Page = example.Page;

            if (example.Language is not null)
            {
                exampleDb.ProgrammingLanguageId = (await _dtoService.GetOrAddLanguage(example.Language, GetUserId())).ProgrammingLanguageId;
            }
            if (example.Type is not null)
            {
                exampleDb.TypeId = (await _dtoService.GetOrAddType(example.Type, GetUserId())).TypeId;
            }

            return NoContent();
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex, $"{nameof(GetExample)}() failed");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("{exampleId}", Name = "ExampleGet")]
    [ProducesResponseType(StatusCodes.Status200OK, Type= typeof(ExampleDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ExampleDto>> GetExample([FromRoute]int exampleId)
    {
        try
        {
            var example = await _exampleRepo.Get(exampleId);
            if (example is null)
            {
                return NotFound();
            }

            var output = example.Adapt<ExampleDto>();

            await _dtoService.AddLangugeAndType(example, output);

            return Ok(output);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, $"{nameof(GetExample)}() failed");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }


    [HttpGet("resource/{resourceId}", Name = "ExampleGetForResource")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ExampleDto>))]
    public async Task<ActionResult<IEnumerable<ExampleDto>>> GetExamples([FromRoute]int resourceId)
    {
        try
        {
            var examples = (await _exampleRepo.GetAll(resourceId)).ToList();
            var output = new List<ExampleDto>();

            foreach (var example in examples)
            {
                var outDto = example.Adapt<ExampleDto>();
                await _dtoService.AddLangugeAndType(example, outDto);
                output.Add(outDto);
            }

            return Ok(output);
        } catch (Exception ex)
        {
            _logger.LogWarning(ex, $"{nameof(GetExamples)}() Failed");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPost(Name = "ExampleAdd")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ExampleDto))]
    public async Task<ActionResult<ExampleDto>> AddExample([FromBody]CreateExampleRequest exampleRequest)
    {
        try
        {
            var newExample = exampleRequest.Adapt<Example>();
            newExample.UserId = GetUserId();

            if(exampleRequest.Language is not null)
            {
                newExample.ProgrammingLanguageId = (await _dtoService.GetOrAddLanguage(exampleRequest.Language, GetUserId())).ProgrammingLanguageId;
            }
            if(exampleRequest.Type is not null)
            {
                newExample.TypeId = (await _dtoService.GetOrAddType(exampleRequest.Type, GetUserId())).TypeId;
            }

            var output = await _exampleRepo.Save(newExample);
            return CreatedAtAction(nameof(GetExample), new { exampleId = output.ExampleId }, output.Adapt<ExampleDto>());
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, $"{nameof(AddExample)}() Failed");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpDelete("{exampleId}",Name = "ExampleDelete")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteExample([FromRoute]int exampleId)
    {
        // TODO: Role for deleting
        try
        {
            await _exampleRepo.Delete(exampleId);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, $"{nameof(DeleteExample)}() Failed");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    private string GetUserId()
    {
        return User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
    }
}
