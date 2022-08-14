using Microsoft.AspNetCore.Mvc;
using ProgrammingResourcesLibrary.Models;
using ProgrammingResourcesLibrary.Repositories.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProgrammingResourcesApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ResourceTagController : ControllerBase
{
    private readonly IResourceTagRepo _resourceTagRepo;

    public ResourceTagController(IResourceTagRepo resourceTagRepo)
    {
        _resourceTagRepo = resourceTagRepo;
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
    public async Task<ActionResult<ResourceTag>> Post([FromBody] ResourceTag rt)
    {
        await _resourceTagRepo.Save(rt);
        var savedRt = await _resourceTagRepo.Get(rt.ResourceTagId);
        if(savedRt is null)
        {
            return BadRequest();
        }

        return savedRt;
    }

    // DELETE api/<ResourceTagController>/5
    [HttpDelete("{id}")]
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
