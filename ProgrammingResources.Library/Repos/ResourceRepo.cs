using Dapper;
using Microsoft.Extensions.Options;
using ProgrammingResources.Library.DependencyInjection;
using ProgrammingResources.Library.Models;
using ProgrammingResources.Library.Services.Repos;
using System.Data;
using System.Data.SqlClient;


namespace ProgrammingResources.Library.Services;

public class ResourceRepo : IResourceRepo
{
    private readonly ProgrammingResourcesOptions _options;

    public ResourceRepo(IOptions<ProgrammingResourcesOptions> options)
    {
        _options = options.Value;
    }

    public async Task<IEnumerable<Resource>> GetAll()
    {
        using IDbConnection connection = new SqlConnection(_options.ConnectionString);

        var resources = await connection.QueryAsync<Resource>("dbo.spResource_GetAll",
            commandType: CommandType.StoredProcedure);

        return resources;
    }

    public async Task<Resource?> Get(int resourceId)
    {
        using IDbConnection connection = new SqlConnection(_options.ConnectionString);

        var resource = await connection.QuerySingleOrDefaultAsync<Resource>("dbo.spResource_Get",
            new { resourceId },
            commandType: CommandType.StoredProcedure);

        return resource;
    }

    public async Task<Resource> Save(Resource resource)
    {
        using IDbConnection connection = new SqlConnection(_options.ConnectionString);

        var output = await connection.QuerySingleAsync<Resource>("dbo.spResource_Upsert",
            new
            {
                ResourceId = resource.ResourceId,
                Title = resource.Title,
                Url = resource.Url,
                Description = resource.Description,
                ProgrammingLanguageId = resource.ProgrammingLanguageId,
                TypeId = resource.TypeId,
                UserId = resource.UserId,
            },
            commandType: CommandType.StoredProcedure);

        return output;
    }

    public async Task Delete(int resourceId)
    {
        using IDbConnection connection = new SqlConnection(_options.ConnectionString);

        await connection.ExecuteAsync("dbo.spResource_Delete",
            new { resourceId },
            commandType: CommandType.StoredProcedure);
    }
}
