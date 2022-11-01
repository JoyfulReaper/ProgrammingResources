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
public class ProgrammingLanguageController : ControllerBase
{
    private readonly IProgrammingLanguageRepo _programmingLanguageRepo;
    private readonly ILogger<ProgrammingLanguageController> _logger;

    public ProgrammingLanguageController(IProgrammingLanguageRepo programmingLanguageService,
        ILogger<ProgrammingLanguageController> logger)
    {
        _programmingLanguageRepo = programmingLanguageService;
        _logger = logger;
    }

    [HttpGet(Name = "ProgrammingLanguageGetAll")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<string>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<string>>> GetAll()
    {
        try
        {
            var programmingLanguages = (await _programmingLanguageRepo.GetAll())
                .ToList();
            var output = new List<string>();
            programmingLanguages.ForEach(pl => output.Add(pl.Language));

            return Ok(output);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "GetAll Failed.");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    //[HttpGet("{programmingLanguageId}", Name = "ProgrammingLanguageGet")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //public async Task<ActionResult<ProgrammingLanguage>> Get(int programmingLanguageId)
    //{
    //    try
    //    {
    //        var programmingLanguage = (await _programmingLanguageRepo.Get(programmingLanguageId));
    //        if (programmingLanguage is null)
    //        {
    //            return NotFound();
    //        }

    //        return Ok(programmingLanguage.Language);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogWarning(ex, "Get Failed.");
    //        return StatusCode(StatusCodes.Status500InternalServerError);
    //    }
    //}

    [HttpPut (Name="ProgrammingLanguageAdd")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ProgrammingLanguage>> AddLangauge(string programmingLanguage)
    {
        try
        {
            if((await _programmingLanguageRepo.Get(programmingLanguage)) is not null)
            {
                return BadRequest("Programming langauge exists");
            }

            var pl = new ProgrammingLanguage()
            {
                Language = programmingLanguage
            };

            pl.UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
            var output = await _programmingLanguageRepo.Add(pl);
            return Ok(output);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, $"{nameof(AddLangauge)}() Failed.");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

    }

    [HttpDelete ("{programmingLangauge}", Name = "ProgrammingLanguageDelete")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(string programmingLangauge)
    {
        // TODO add a role that allows deleting
        try
        {
            var pl = await _programmingLanguageRepo.Get(programmingLangauge);
            if(pl is null)
            {
                return NotFound();
            }

            await _programmingLanguageRepo.Delete(pl.ProgrammingLanguageId);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Delete Failed");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
