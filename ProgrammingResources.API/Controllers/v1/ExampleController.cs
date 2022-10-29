using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProgrammingResources.API.DTOs;
using ProgrammingResources.Library.Models;
using ProgrammingResources.Library.Services.Interfaces;

namespace ProgrammingResources.API.Controllers.v1;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiController]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
public class ExampleController : ControllerBase
{
    private readonly IExampleService _exampleService;
    private readonly ILogger<ExampleController> _logger;

    public ExampleController(IExampleService exampleService,
        ILogger<ExampleController> logger)
    {
        _exampleService = exampleService;
        _logger = logger;
    }

    [HttpGet("{exampleId}", Name = "ExampleGet")]
    [ProducesResponseType(StatusCodes.Status200OK, Type= typeof(ExampleDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ExampleDto>> GetExample(int exampleId)
    {
        try
        {
            var example = await _exampleService.Get(exampleId);
            if (example is null)
            {
                return NotFound();
            }

            return Ok(example.Adapt<ExampleDto>());
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "GetExample() failed");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }


    [HttpGet("{resourceId}", Name = "ExampleGet")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ExampleDto>))]
    public async Task<ActionResult<IEnumerable<ExampleDto>>> GetExamples(int resourceId)
    {
        try
        {
            var examples = (await _exampleService.GetAll(resourceId)).ToList();
            var output = new List<ExampleDto>();
            examples.ForEach(e => output.Add(e.Adapt<ExampleDto>()));

            return Ok(output);
        } catch (Exception ex)
        {
            _logger.LogWarning(ex, "GetExamples() Failed");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPost(Name = "AddExample")]
    public async Task<ActionResult<ExampleDto>> AddExample(CreateExampleRequest exampleRequest)
    {
        try
        {
            var example = await _exampleService.Save(exampleRequest.Adapt<Example>());
            return CreatedAtAction(nameof(GetExample), new { exampleId = example.ExampleId }, example.Adapt<ExampleDto>());
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "AddExample() Failed");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
