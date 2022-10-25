using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ProgrammingResources.API.ServiceSetup;

public static class Identity
{
    public static void AddIdentity(this WebApplicationBuilder builder)
    {
        // Identity
        builder.Services.AddDbContext<IdentityDbContext>(opts =>
        {
            opts.UseSqlServer(builder.Configuration.GetConnectionString("ProgrammingApiIdentity"),
                opts => opts.MigrationsAssembly("ProgrammingResources.API")
                );
        });

        builder.Services.AddIdentity<IdentityUser, IdentityRole>(opts =>
        {
            opts.SignIn.RequireConfirmedAccount = false;
        }).AddEntityFrameworkStores<IdentityDbContext>()
        .AddDefaultTokenProviders();
    }
}
