﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ProgrammingResources.API.Identity;
using ProgrammingResources.API.Models;
using ProgrammingResources.API.Options;
using ProgrammingResources.API.Services;

namespace ProgrammingResources.API.Controllers.v1;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiController]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
public class TokenController : ControllerBase
{
    private readonly ITokenService _tokenService;
    private readonly UserManager<ApiIdentityUser> _userManager;
    private readonly JwtOptions _jwtOptions;

    public TokenController(ITokenService tokenService,
        UserManager<ApiIdentityUser> userManager,
        IOptions<JwtOptions> jwtOptions)
    {
        _tokenService = tokenService;
        _userManager = userManager;
        _jwtOptions = jwtOptions.Value;
    }

    [HttpPost("TokenRefresh")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Refresh(TokenApiModel tokenApiModel)
    {
        if (tokenApiModel?.RefreshToken is null ||
            tokenApiModel?.AccessToken is null)
        {
            return BadRequest("Invalid client request");
        }

        string accessToken = tokenApiModel.AccessToken;
        string refreshToken = tokenApiModel.RefreshToken;

        var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);
        var username = principal.Identity!.Name;
        var user = await _userManager.FindByNameAsync(username);

        if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
        {
            return BadRequest("Invalid client request");
        }

        var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims);
        var newRefreshToken = _tokenService.GenerateRefreshToken();
        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiryTime = DateTime.Now.AddDays(_jwtOptions.RefreshExpirationDays);

        await _userManager.UpdateAsync(user);
        return Ok(new AuthenticatedResponse()
        {
            Token = newAccessToken,
            RefreshToken = newRefreshToken
        });
    }

    [HttpPost]
    [Route("TokenRevoke")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Revoke()
    {
        var username = User.Identity!.Name;
        var user = await _userManager.FindByNameAsync(username);
        if (user == null)
        {
            return BadRequest();
        }

        user.RefreshToken = null;
        await _userManager.UpdateAsync(user);

        return NoContent();
    }
}
