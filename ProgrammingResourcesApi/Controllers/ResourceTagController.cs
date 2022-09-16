using Microsoft.AspNetCore.Mvc;
using ProgrammingResourcesApi.DTOs;
using ProgrammingResourcesLibrary.Models;
using ProgrammingResourcesLibrary.Repositories.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProgrammingResourcesApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ResourceTagController : ControllerBase
{
    private readonly IResourceTagRepo _resourceTagRepo;
    private readonly ITagRepo _tagRepo;

    public ResourceTagController(IResourceTagRepo resourceTagRepo,
        ITagRepo tagRepo)
    {
        _resourceTagRepo = resourceTagRepo;
        _tagRepo = tagRepo;
    }

    [HttpPost("TagResource")]
    public async Task<IActionResult> TagResource(TagResourceDto tagResource)
    {
        var tag = new Tag
        {
            Name = tagResource.TagName
        };

        await _tagRepo.Save(tag);
        var savedTag = await _tagRepo.Get(tag.TagId);
        if(savedTag is null)
        {
            return BadRequest();
        }

        ResourceTag rt = new()
        {
            ResourceId = tagResource.ResourceId,
            TagId = savedTag.TagId
        };

        await _resourceTagRepo.Save(rt);
        var savedRt = await _resourceTagRepo.Get(rt.ResourceTagId);
        if(savedRt is null)
        {
            return BadRequest();
        }

        return NoContent();
    }

    [HttpGet("Tagged/{tagId}")]
    public async Task<IEnumerable<Resource>> GetTaggedResources(int tagId)
    {
        return await _resourceTagRepo.GetTagged(tagId);
    }

    // GET: api/<ResourceTagController>
    [HttpGet]
    public async Task<IEnumerable<ResourceTag>> Get()
    {
        return await _resourceTagRepo.GetAll();
    }

    // GET api/<ResourceTagController>/5
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResourceTag))]
    public async Task<ActionResult<ResourceTag>> Get(int id)
    {
        var rt = await _resourceTagRepo.Get(id);
        if (rt == null)
        {
            return NotFound();
        }

        return rt;
    }

    // POST api/<ResourceTagController>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResourceTag))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ResourceTag>> Post([FromBody] ResourceTag rt)
    {
        await _resourceTagRepo.Save(rt);
        var savedRt = await _resourceTagRepo.Get(rt.ResourceTagId);
        if(savedRt is null)
        {
            return BadRequest();
        }

        return CreatedAtAction(nameof(Get), new { savedRt.ResourceTagId }, savedRt);
    }

    // DELETE api/<ResourceTagController>/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(int id)
    {
        var rt = await _resourceTagRepo.Get(id);
        if(rt is null)
        {
            return NotFound();
        }

        await _resourceTagRepo.Delete(id);
        return NoContent();
    }
}
