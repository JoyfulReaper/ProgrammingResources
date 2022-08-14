using Microsoft.AspNetCore.Mvc;
using ProgrammingResourcesLibrary.Models;
using ProgrammingResourcesLibrary.Repositories.Interfaces;

namespace ProgrammingResourcesApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ResourceController : ControllerBase
{
    private readonly IResourceRepo _resourceRepo;

    public ResourceController(IResourceRepo resourceRepo)
    {
        _resourceRepo = resourceRepo;
    }

    // GET: api/<ResourceController>
    [HttpGet]
    public async Task<IEnumerable<Resource>> Get()
    {
        return await _resourceRepo.GetAll();
    }

    // GET api/<ResourceController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Resource>> Get(int id)
    {
        var resource = await _resourceRepo.Get(id);
        if(resource == null)
        {
            return NotFound();
        }

        return resource;
    }

    // POST api/<ResourceController>
    [HttpPost]
    public async Task<ActionResult<Resource>> Post([FromBody] Resource resource)
    {
        await _resourceRepo.Save(resource);
        var savedResource = await _resourceRepo.Get(resource.ResourceId);
        if(savedResource == null)
        {
            return BadRequest();
        }
        return savedResource;
    }

    // PUT api/<ResourceController>/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] Resource resource)
    {
        if(id != resource.ResourceId)
        {
            return BadRequest();
        }

        await _resourceRepo.Save(resource);
        return NoContent();
    }

    // DELETE api/<ResourceController>/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _resourceRepo.Delete(id);
        return NoContent();
    }
}
