﻿using Microsoft.OpenApi.Models;
using System.Reflection;

namespace ProgrammingResources.API.ServiceSetup;

public static class Swagger
{
    public static void AddSwagger(this WebApplicationBuilder builder)
    {
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

        var securityScheme = new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Description = "JWT Authorization header info using bearer tokens",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT"
        };

        var securityRequirement = new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "bearerAuth"
                    }
                },
                new string[] { }
            }
        };

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(opts =>
        {
            var title = "Programming Resources";
            var description = "API for collecting and orginazing programming resources and examples";
            var terms = new Uri("https://opensource.org/licenses/MIT");
            var license = new OpenApiLicense()
            {
                Name = "MIT",
                Url = new Uri("https://opensource.org/licenses/MIT"),
            };

            OpenApiContact contact = new()
            {
                Name = "Kyle Givler",
                Url = new Uri("https://github.com/JoyfulReaper/ProgrammingResources")
            };

            opts.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = $"{title} v1",
                Description = description,
                TermsOfService = terms,
                License = license,
                Contact = contact
            });

            //opts.SwaggerDoc("v2", new OpenApiInfo
            //{
            //    Version = "v2",
            //    Title = $"{title} v2",
            //    Description = description,
            //    TermsOfService = terms,
            //    License = License
            //});

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            opts.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFile));

            opts.AddSecurityDefinition("bearerAuth", securityScheme);
            opts.AddSecurityRequirement(securityRequirement);
        });

        builder.Services.AddApiVersioning(opts =>
        {
            opts.AssumeDefaultVersionWhenUnspecified = true;
            opts.DefaultApiVersion = new(1, 0);
            opts.ReportApiVersions = true;
        });

        builder.Services.AddVersionedApiExplorer(opts =>
        {
            opts.GroupNameFormat = "'v'VVV";
            opts.SubstituteApiVersionInUrl = true;
        });
    }
}
