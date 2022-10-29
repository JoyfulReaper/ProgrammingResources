using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Options;
using ProgrammingResources.Library.DependencyInjection;
using Dapper;
using ProgrammingResources.Library.Models;
using ProgrammingResources.Library.Services.Interfaces;

namespace ProgrammingResources.Library.Services;

public class TagService : ITagService
{
    private readonly ProgrammingResourcesOptions _options;

    public TagService(IOptions<ProgrammingResourcesOptions> options)
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

        var tag = (await connection.QueryAsync<Tag>("dbo.spTag_Get",
            new { tagId },
            commandType: CommandType.StoredProcedure))
            .SingleOrDefault();

        return tag;
    }

    public async Task<Tag> Add(Tag tag)
    {
        using IDbConnection connection = new SqlConnection(_options.ConnectionString);

        Tag output = (await connection.QueryAsync<Tag>("dbo.spTag_Insert",
            new { UserId = tag.UserId, Name = tag.Name },
            commandType: CommandType.StoredProcedure))
            .Single();

        return output;
    }

    public async Task Delete(int tagId)
    {
        using IDbConnection connection = new SqlConnection(_options.ConnectionString);

        await connection.ExecuteAsync("dbo.spTag_Delete",
            new { tagId },
            commandType: CommandType.StoredProcedure);
    }
}
