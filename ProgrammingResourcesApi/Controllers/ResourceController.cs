using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProgrammingResourcesApi.DTOs;
using ProgrammingResourcesLibrary.Models;
using ProgrammingResourcesLibrary.Repositories.Interfaces;

namespace ProgrammingResourcesApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ResourceController : ControllerBase
{
    private readonly IResourceRepo _resourceRepo;
    private readonly IMapper _mapper;

    public ResourceController(IResourceRepo resourceRepo,
        IMapper mapper)
    {
        _resourceRepo = resourceRepo;
        _mapper = mapper;
    }

    // GET: api/<ResourceController>
    [HttpGet]
    public async Task<IEnumerable<ResourceDto>> Get()
    {
        var resource = await _resourceRepo.GetAll();
        return _mapper.Map<IEnumerable<ResourceDto>>(resource);
    }

    // GET api/<ResourceController>/5
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(ResourceDto))]
    public async Task<ActionResult<ResourceDto>> Get(int id)
    {
        var resource = await _resourceRepo.Get(id);
        if(resource == null)
        {
            return NotFound();
        }

        return _mapper.Map<ResourceDto>(resource);
    }

    // POST api/<ResourceController>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResourceDto))]
    public async Task<ActionResult<ResourceDto>> Post([FromBody] ResourceDto resourceCreateDto)
    {
        var resource = _mapper.Map<Resource>(resourceCreateDto);
        await _resourceRepo.Save(resource);
        var savedResource = await _resourceRepo.Get(resource.ResourceId);
        if(savedResource == null)
        {
            return BadRequest();
        }
        var output = _mapper.Map<ResourceDto>(savedResource);
        return CreatedAtAction(nameof(Get), new { id = output.ResourceId }, output);
    }

    // PUT api/<ResourceController>/5
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Put(int id, [FromBody] ResourceDto resource)
    {
        if(id != resource.ResourceId)
        {
            return BadRequest();
        }

        await _resourceRepo.Save(_mapper.Map<Resource>(resource));
        return NoContent();
    }

    // DELETE api/<ResourceController>/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var resource = await _resourceRepo.Get(id);
        if (resource is null)
        {
            return NotFound();
        }

        await _resourceRepo.Delete(id);
        return NoContent();
    }
}
