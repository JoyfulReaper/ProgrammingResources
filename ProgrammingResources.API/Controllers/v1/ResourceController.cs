using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProgrammingResources.API.DTOs;
using ProgrammingResources.Library.Services.Repos;

namespace ProgrammingResources.API.Controllers.v1;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiController]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
public class ResourceController : ControllerBase
{
    private readonly IResourceRepo _resourceRepo;
    private readonly ILogger<ResourceController> _logger;

    public ResourceController(IResourceRepo resourceService,
		ILogger<ResourceController> logger)
	{
        _resourceRepo = resourceService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ResourceDto>>> GetAllResources()
    {
        throw new NotImplementedException();
        var allResources = await _resourceRepo.GetAll();
    }
}
