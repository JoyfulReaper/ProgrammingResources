using Microsoft.Extensions.DependencyInjection;
using ProgrammingResources.ApiClient.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingResources.ApiClient.DependencyInjection;
public static class AddApiClient
{
    public static IServiceCollection AddProgrammingResourceClient(this IServiceCollection services)
    {
        services.AddTransient<IExampleEndpoint, ExampleEndpoint>();
        services.AddTransient<IProgrammingLanguageEndpoint, ProgrammingLanguageEndpoint>();
        services.AddTransient<ITagEndpoint, TagEndpoint>();
        services.AddTransient<ITypeEndpoint, TypeEndpoint>();
        //services.AddTransient<IResourceEndpoint, ResourceEndpoint>();

        return services;
    }
}
