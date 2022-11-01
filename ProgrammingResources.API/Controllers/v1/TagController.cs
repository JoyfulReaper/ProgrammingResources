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
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<string>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<Tag>>> GetAll()
    {
        try
        {
            var tags = (await _tagService.GetAll())
                .ToList();
            var output = new List<string>();
            tags.ForEach(t => output.Add(t.Name));

            return Ok(output);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, $"{nameof(GetAll)}() failed");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Add a new tag
    /// </summary>
    /// <param name="name"></param>
    /// <returns>Bad Request if the tag already exists</returns>
    [HttpPut(Name = "TagAdd")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<string>> AddTag(string name)
    {
        try
        {
            var tag = await _tagService.Get(name);
            if (tag is not null)
            {
                return BadRequest("Tag Exists");
            }

            var output = (await _tagService.Add(new Tag
            {
                Name = name,
                UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value
            }));

            return output.Name;
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, message: $"{nameof(AddTag)}() failed");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPut("tagResource", Name = "TagResource")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> TagResource([FromBody] TagResourceRequest tagResourceRequest)
    {
        try
        {
            await _tagService.TagResource(tagResourceRequest.TagId, tagResourceRequest.ResourceId, User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, message: $"{nameof(TagResource)}() failed");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpDelete("{tag}", Name = "TagDelete")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> Delete(string tag)
    {
        try
        {
            var tagDb = await _tagService.Get(tag);
            if(tagDb is null)
            {
                return NotFound();
            }

            await _tagService.Delete(tagDb.TagId);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, message: $"{nameof(Delete)}() failed");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
