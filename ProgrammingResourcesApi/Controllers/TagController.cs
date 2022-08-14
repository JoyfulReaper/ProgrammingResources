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
    public async Task<IEnumerable<Tag>> Get()
    {
        return await _tagRepo.GetAll();
    }

    // GET api/<TagController>/5
    [HttpGet("{id}")]
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
    public async Task<ActionResult<Tag>> Post([FromBody] Tag tag)
    {
        await _tagRepo.Save(tag);
        var savedTag = await _tagRepo.Get(tag.TagId);
        if(savedTag == null)
        {
            return BadRequest();
        }

        return savedTag;
    }

}
