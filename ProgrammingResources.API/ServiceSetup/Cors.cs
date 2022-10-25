﻿namespace ProgrammingResources.API.ServiceSetup;

public static class Cors
{
    public static void AddCors(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(opts =>
        {
            var origin = builder.Configuration.GetValue<string>("CorsOrigin");

            opts.AddDefaultPolicy(builder =>
                builder.WithOrigins(origin)
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
                );
        });
    }
}
