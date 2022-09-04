using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProgrammingResourcesApi.DTOs;
using ProgrammingResourcesLibrary.Models;
using ProgrammingResourcesLibrary.Repositories.Interfaces;

namespace ProgrammingResourcesApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ExampleController : ControllerBase
{
    private readonly IExampleRepo _exampleRepo;
    private readonly IMapper _mapper;

    public ExampleController(IExampleRepo exampleRepo, IMapper mapper)
    {
        _exampleRepo = exampleRepo;
        _mapper = mapper;
    }

    // GET api/<ExampleController>/5
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ExampleDto))]
    public async Task<ActionResult<ExampleDto>> Get(int resourceId)
    {
        var example = await _exampleRepo.GetByResource(resourceId);
        if (example is null)
        {
            return NotFound();
        }
        return _mapper.Map<ExampleDto>(example);
    }

    // POST api/<ExampleController>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ExampleDto))]
    public async Task<ActionResult<ExampleDto>> Post([FromBody] ExampleDto exampleDto)
    {
        var example = _mapper.Map<Example>(exampleDto);
        await _exampleRepo.Save(example);
        var savedExample = await _exampleRepo.GetByResource(example.ExampleId);
        if (savedExample is null)
        {
            return BadRequest();
        }

        return _mapper.Map<ExampleDto>(example);
    }

    // PUT api/<ExampleController>/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] ExampleDto exampleDto)
    {
        if(id != exampleDto.ExampleId)
        {
            return BadRequest();
        }

        await _exampleRepo.Save(_mapper.Map<Example>(exampleDto));
        return NoContent();
    }

    // DELETE api/<ExampleController>/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var example = await _exampleRepo.Get(id);
        if(example is null)
        {
            return NotFound();
        }

        await _exampleRepo.Delete(id);
        return NoContent();
    }
}
