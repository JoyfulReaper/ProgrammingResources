using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ProgrammingResources.API.Identity;
using ProgrammingResources.API.Models;
using ProgrammingResources.API.Options;
using ProgrammingResources.API.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace ProgrammingResources.API.Controllers.v1;
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly UserManager<ApiIdentityUser> _userManager;
    private readonly SignInManager<ApiIdentityUser> _signInManager;
    private readonly ITokenService _tokenService;
    private readonly JwtOptions _jwtOptions;

    public record RegisterRec(string? UserName, string? Password, string? EmailAddress);
    public record LoginRec(string? UserName, string? Password);

    public AuthenticationController(UserManager<ApiIdentityUser> userManager,
        SignInManager<ApiIdentityUser> signInManager,
        IOptions<JwtOptions> jwtOptions,
        ITokenService tokenService)
	{
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
        _jwtOptions = jwtOptions.Value;
    }

    [HttpPost("token", Name = "GetToken")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [AllowAnonymous]
    public async Task<ActionResult<string>> Authenticate([FromBody] LoginRec loginRec)
    {
        var user = await ValidateCredentials(loginRec);

        if (user is null)
        {
            return Unauthorized();
        }

        var token = GenerateToken(user);
        var refreshToken = _tokenService.GenerateRefreshToken();
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.Now.AddDays(_jwtOptions.RefreshExpirationDays);
        await _userManager.UpdateAsync(user);

        return Ok(new AuthenticatedResponse
        {
            Token = token,
            RefreshToken = refreshToken,
        });
    }

    [AllowAnonymous]
    [HttpPost("register", Name = "Register")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] RegisterRec registerRec)
    {
        var user = new ApiIdentityUser
        {
            UserName = registerRec.UserName,
            Email = registerRec.EmailAddress,
        };

        var result = await _userManager.CreateAsync(user, registerRec.Password);
        if (result.Succeeded)
        {
            return NoContent();
        }
        else
        {
            var errorOut = string.Empty;
            foreach (var error in result.Errors)
            {
                errorOut += " " + error.Description;
            }
            return BadRequest(errorOut);
        }
    }

    private async Task<ApiIdentityUser?> ValidateCredentials(LoginRec login)
    {
        ApiIdentityUser user = await _userManager.FindByNameAsync(login.UserName);
        SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, login.Password, true);
        if (result.Succeeded)
        {
            return user;
        }

        return null;
    }

    private string GenerateToken(IdentityUser user)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        return _tokenService.GenerateAccessToken(claims);
    }
}
