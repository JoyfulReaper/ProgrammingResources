using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProgrammingResources.API.DTOs;
using ProgrammingResources.Library.Models;
using ProgrammingResources.Library.Services;
using ProgrammingResources.Library.Services.Interfaces;
using System.Security.Claims;
using Type = ProgrammingResources.Library.Models.Type;

namespace ProgrammingResources.API.Controllers.v1;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiController]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
public class TypeController : ControllerBase
{
    private readonly ITypeService _typeService;
    private readonly ILogger<TypeController> _logger;

    public TypeController(ITypeService typeService,
        ILogger<TypeController> logger)
	{
        _typeService = typeService;
        _logger = logger;
    }

    [HttpGet(Name = "TypeGetAll")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Type>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<Type>>> GetAll()
    {
        try
        {
            var types = (await _typeService.GetAll())
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
            var type = (await _typeService.Get(typeId));
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

    [HttpPost(Name = "TypeInsert")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Type))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ProgrammingLanguage>> Insert(string type)
    {
        try
        {
            var newType = new Type()
            {
                Name = type
            };

            newType.UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value; //YOLO, lol jk that catch will handle it
            var output = await _typeService.Add(newType);
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
            await _typeService.Delete(typeId);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Delete Failed");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
