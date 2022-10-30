using Mapster;
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
public class TypeController : ControllerBase
{
    private readonly ITypeRepo _typeRepo;
    private readonly ILogger<TypeController> _logger;

    public TypeController(ITypeRepo typeService,
        ILogger<TypeController> logger)
	{
        _typeRepo = typeService;
        _logger = logger;
    }

    [HttpGet(Name = "TypeGetAll")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Type>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<Type>>> GetAll()
    {
        try
        {
            var types = (await _typeRepo.GetAll())
                .ToList();
            var output = new List<TypeDto>();
            types.ForEach(t => output.Add(t.Adapt<TypeDto>()));

            return Ok(output);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "GetAll Failed.");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("{typeId}", Name = "TypeGet")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TypeDto))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProgrammingLanguage>> Get(int typeId)
    {
        try
        {
            var type = (await _typeRepo.Get(typeId));
            if (type is null)
            {
                return NotFound();
            }

            return Ok(type.Adapt<TypeDto>());
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Get Failed.");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPut(Name = "TypeAdd")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Type))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ProgrammingLanguage>> AddType(string type)
    {
        try
        {
            if((await _typeRepo.Get(type) is not null))
            {
                return BadRequest("Type exists");
            }

            var newType = new Type()
            {
                Name = type
            };

            newType.UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value; //YOLO, lol jk that catch will handle it
            var output = await _typeRepo.Add(newType);
            return CreatedAtAction(nameof(Get), new { output.TypeId }, output.Adapt<TypeDto>());
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Insert Failed.");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

    }

    [HttpDelete(Name = "TypeDelete")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int typeId)
    {
        // TODO add a role that allows deleting
        try
        {
            await _typeRepo.Delete(typeId);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Delete Failed");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
