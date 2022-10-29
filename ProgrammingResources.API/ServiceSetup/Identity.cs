using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProgrammingResources.API.Identity;

namespace ProgrammingResources.API.ServiceSetup;

public static class Identity
{
    public static void AddIdentity(this WebApplicationBuilder builder)
    {
        // Identity
        builder.Services.AddDbContext<IdentityContext>(opts =>
        {
            opts.UseSqlServer(builder.Configuration.GetConnectionString("ProgrammingApiIdentity"),
                opts => opts.MigrationsAssembly("ProgrammingResources.API")
                );
        });

        builder.Services.AddIdentity<ApiIdentityUser, IdentityRole>(opts =>
        {
            opts.SignIn.RequireConfirmedAccount = false;
        }).AddEntityFrameworkStores<IdentityContext>()
        .AddDefaultTokenProviders();
    }
}
