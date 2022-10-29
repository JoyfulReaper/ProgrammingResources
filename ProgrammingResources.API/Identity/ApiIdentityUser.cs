using Microsoft.AspNetCore.Identity;

namespace ProgrammingResources.API.Identity;

public class ApiIdentityUser : IdentityUser
{
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
}
