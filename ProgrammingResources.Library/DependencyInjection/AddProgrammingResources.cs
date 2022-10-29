using Microsoft.Extensions.DependencyInjection;
using ProgrammingResources.Library.Services;
using ProgrammingResources.Library.Services.Interfaces;

namespace ProgrammingResources.Library.DependencyInjection;
public static class AddProgrammingResouces
{
    public static IServiceCollection AddProgrammingResources(this IServiceCollection services,
        Action<ProgrammingResourcesOptions>? setupAction = null)
    {
        services.AddTransient<IProgrammingLanguageService, ProgrammingLanguageService>();
        services.AddTransient<ITypeService, TypeService>();
        services.AddTransient<ITagService, TagService>();
        services.AddTransient<IResourceService, ResourceService>();
        services.AddTransient<IExampleService, ExampleService>();

        if(setupAction is not null )
        {
            services.Configure(setupAction);
        }

        return services;
    }
}
