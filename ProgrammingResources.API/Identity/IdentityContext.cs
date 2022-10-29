using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ProgrammingResources.API.Identity;

public class IdentityContext : IdentityDbContext<ApiIdentityUser>
{
    public IdentityContext(DbContextOptions<IdentityContext> options) : base(options) { }
}
