using Mapster;
using Microsoft.AspNetCore.Mvc;
using ProgrammingResources.API.DTOs;
using ProgrammingResources.Library.Models;
using ProgrammingResources.Library.Services.Repos;

namespace ProgrammingResources.API.Controllers.v1;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiController]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
public class ExampleController : ControllerBase
{
    private readonly IExampleRepo _exampleRepo;
    private readonly ILogger<ExampleController> _logger;

    public ExampleController(IExampleRepo exampleRepo,
        ILogger<ExampleController> logger)
    {
        _exampleRepo = exampleRepo;
        _logger = logger;
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

            return Ok(example.Adapt<ExampleDto>());
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
            examples.ForEach(e => output.Add(e.Adapt<ExampleDto>()));

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
            var example = await _exampleRepo.Save(exampleRequest.Adapt<Example>());
            return CreatedAtAction(nameof(GetExample), new { exampleId = example.ExampleId }, example.Adapt<ExampleDto>());
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
}
