using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProgrammingResources.API.DTOs;
using ProgrammingResources.Library.Models;
using ProgrammingResources.Library.Services.Repos;
using System.Security.Claims;

namespace ProgrammingResources.API.Controllers.v1;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiController]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
public class TagController : ControllerBase
{
    private readonly ITagRepo _tagService;
    private readonly ILogger<TagController> _logger;

    public TagController(ITagRepo tagService,
        ILogger<TagController> logger)
	{
        _tagService = tagService;
        _logger = logger;
    }

    [HttpGet(Name = "TagGetAll")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TagDto>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<Tag>>> GetAll()
    {
        try
        {
            var tags = (await _tagService.GetAll())
                .ToList();
            var output = new List<TagDto>();
            tags.ForEach(t => output.Add(t.Adapt<TagDto>()));

            return Ok(output);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "GetAll failed");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPut(Name = "TagInsert")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TagDto))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<TagDto>> Insert(string name)
    {
        try
        {
            var output = (await _tagService.Add(new Tag
            {
                Name = name,
                UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value
            })).Adapt<TagDto>();

            return output;
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Insert failed");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

}
