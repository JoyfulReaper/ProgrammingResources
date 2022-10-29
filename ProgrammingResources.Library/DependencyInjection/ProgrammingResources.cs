using Microsoft.Extensions.DependencyInjection;
using ProgrammingResources.Library.Services;
using ProgrammingResources.Library.Services.Repos;

namespace ProgrammingResources.Library.DependencyInjection;
public static class AddProgrammingResouces
{
    public static IServiceCollection AddProgrammingResources(this IServiceCollection services,
        Action<ProgrammingResourcesOptions>? setupAction = null)
    {
        services.AddTransient<IProgrammingLanguageRepo, ProgrammingLanguageRepo>();
        services.AddTransient<ITypeRepo, TypeRepo>();
        services.AddTransient<ITagRepo, TagRepo>();
        services.AddTransient<IResourceRepo, ResourceRepo>();
        services.AddTransient<IExampleRepo, ExampleRepo>();

        if(setupAction is not null )
        {
            services.Configure(setupAction);
        }

        return services;
    }
}
