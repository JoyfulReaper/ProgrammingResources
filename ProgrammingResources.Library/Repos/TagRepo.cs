using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Options;
using ProgrammingResources.Library.DependencyInjection;
using Dapper;
using ProgrammingResources.Library.Models;
using ProgrammingResources.Library.Services.Repos;

namespace ProgrammingResources.Library.Services;

public class TagRepo : ITagRepo
{
    private readonly ProgrammingResourcesOptions _options;

    public TagRepo(IOptions<ProgrammingResourcesOptions> options)
    {
        _options = options.Value;
    }

    public async Task<IEnumerable<Tag>> GetAll()
    {
        using IDbConnection connection = new SqlConnection(_options.ConnectionString);

        var tags = await connection.QueryAsync<Tag>("dbo.spTag_GetAll",
            commandType: CommandType.StoredProcedure);

        return tags;
    }

    public async Task<Tag?> Get(int tagId)
    {
        using IDbConnection connection = new SqlConnection(_options.ConnectionString);

        var tag = await connection.QuerySingleOrDefaultAsync<Tag>("dbo.spTag_Get",
            new { tagId },
            commandType: CommandType.StoredProcedure);

        return tag;
    }

    public async Task<IEnumerable<Tag>> GetByResource(int resourceId)
    {
        using IDbConnection connection = new SqlConnection(_options.ConnectionString);
        var tags = await connection.QueryAsync<Tag>("dbo.spTag_GetByResource",
            new { resourceId },
            commandType: CommandType.StoredProcedure);

        return tags;
    }

    public async Task<Tag> Add(Tag tag)
    {
        using IDbConnection connection = new SqlConnection(_options.ConnectionString);

        Tag output = await connection.QuerySingleAsync<Tag>("dbo.spTag_Insert",
            new { UserId = tag.UserId, Name = tag.Name },
            commandType: CommandType.StoredProcedure);

        return output;
    }

    public async Task Delete(int tagId)
    {
        using IDbConnection connection = new SqlConnection(_options.ConnectionString);

        await connection.ExecuteAsync("dbo.spTag_Delete",
            new { tagId },
            commandType: CommandType.StoredProcedure);
    }

    public async Task TagResource(int tagId, int resourceId, string userId)
    {
        using IDbConnection connection = new SqlConnection(_options.ConnectionString);

        await connection.ExecuteAsync("dbo.spResourceTag_Insert",
            new
            {
                TagId = tagId,
                ResourceId = resourceId,
                UserId = userId
            },
            commandType: CommandType.StoredProcedure);
    }

    public async Task UntagResource(int tagId, int resourceId)
    {
        using IDbConnection connection = new SqlConnection(_options.ConnectionString);

        await connection.ExecuteAsync("dbo.spResourceTag_Delete",
            new
            {
                TagId = tagId,
                ResourceId = resourceId,
            },
            commandType: CommandType.StoredProcedure);
    }
}
