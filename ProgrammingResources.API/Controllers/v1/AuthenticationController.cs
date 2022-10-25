using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ProgrammingResources.API.Options;
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
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly JwtOptions _jwtOptions;

    public record RegisterRec(string? UserName, string? Password, string? EmailAddress);
    public record LoginRec(string? UserName, string? Password);

    public AuthenticationController(UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager,
        IOptions<JwtOptions> jwtOptions)
	{
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtOptions = jwtOptions.Value;
    }

    [HttpGet("TestLogin", Name = "TestLogin")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    public IActionResult TestLogin()
    {
        return Ok("You are logged in!");
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
        return Ok(token);
    }

    [AllowAnonymous]
    [HttpPost("register", Name = "Register")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] RegisterRec registerRec)
    {
        var user = new IdentityUser
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

    private async Task<IdentityUser?> ValidateCredentials(LoginRec login)
    {
        IdentityUser user = await _userManager.FindByNameAsync(login.UserName);
        SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, login.Password, true);
        if (result.Succeeded)
        {
            return user;
        }

        return null;
    }

    private string GenerateToken(IdentityUser user)
    {
        var secretKey = new SymmetricSecurityKey(
            Encoding.ASCII.GetBytes(
                _jwtOptions.SecretKey));

        var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        List<Claim> claims = new List<Claim>();
        claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
        claims.Add(new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName));

        var token = new JwtSecurityToken(
            _jwtOptions.Issuer,
            _jwtOptions.Audience,
            claims,
            DateTime.UtcNow,
            DateTime.UtcNow.AddMinutes(_jwtOptions.ExpirationMinutes),
            signingCredentials);

        return new JwtSecurityTokenHandler()
            .WriteToken(token);
    }
}
