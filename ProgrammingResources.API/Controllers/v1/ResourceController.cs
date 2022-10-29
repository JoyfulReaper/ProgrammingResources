using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProgrammingResources.API.Controllers.v1;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiController]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
public class ResourceController : ControllerBase
{
	public ResourceController()
	{

	}
}
