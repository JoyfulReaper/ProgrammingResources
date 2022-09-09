using Microsoft.AspNetCore.Mvc;
using ProgrammingResourcesLibrary.Models;
using ProgrammingResourcesLibrary.Repositories.Interfaces;

namespace ProgrammingResourcesApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TagController : ControllerBase
{
    private readonly ITagRepo _tagRepo;

    public TagController(ITagRepo tagRepo)
    {
        _tagRepo = tagRepo;
    }

    // GET: api/<TagController>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Tag>))]
    public async Task<IEnumerable<Tag>> Get()
    {
        return await _tagRepo.GetAll();
    }

    // GET api/<TagController>/5
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(Tag))]
    public async Task<ActionResult<Tag>> Get(int id)
    {
        var tag = await _tagRepo.Get(id);
        if(tag == null)
        {
            return NotFound();
        }

        return tag;
    }

    // POST api/<TagController>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<Tag>> Post([FromBody] Tag tag)
    {
        await _tagRepo.Save(tag);
        var savedTag = await _tagRepo.Get(tag.TagId);
        if(savedTag == null)
        {
            return BadRequest();
        }

        return CreatedAtAction(nameof(Get), new { id = savedTag.TagId }, savedTag);
    }

    // POST api/<TagController>
    [HttpPost("multiple")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<Tag>> PostMultiple([FromBody] List<Tag> tags)
    {
        var output = new List<Tag>();
        foreach(var tag in tags)
        {
            await _tagRepo.Save(tag);
            var savedTag = await _tagRepo.Get(tag.TagId);
            output.Add(savedTag);
        }

        return Ok(output);
    }

}