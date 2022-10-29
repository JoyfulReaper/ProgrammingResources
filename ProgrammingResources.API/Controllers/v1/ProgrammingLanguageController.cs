using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProgrammingResources.API.DTOs;
using ProgrammingResources.Library.Models;
using ProgrammingResources.Library.Services.Interfaces;

namespace ProgrammingResources.API.Controllers.v1;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiController]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
public class ProgrammingLanguageController : ControllerBase
{
    private readonly IProgrammingLanguageService _programmingLanguageService;
    private readonly ILogger<ProgrammingLanguageController> _logger;

    public ProgrammingLanguageController(IProgrammingLanguageService programmingLanguageService,
        ILogger<ProgrammingLanguageController> logger)
    {
        _programmingLanguageService = programmingLanguageService;
        _logger = logger;
    }

    [HttpGet(Name = "GetAll")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProgrammingLanguageDto))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]

    public async Task<ActionResult<IEnumerable<ProgrammingLanguage>>> GetAll()
    {
        try
        {
            var programmingLanguages = (await _programmingLanguageService.GetAll())
                .ToList();
            var output = new List<ProgrammingLanguageDto>();
            programmingLanguages.ForEach(pl => output.Add(pl.Adapt<ProgrammingLanguageDto>()));

            return Ok(output);
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex, "GetAll Failed.");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
