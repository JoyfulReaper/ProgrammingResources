using Dapper;
using Microsoft.Extensions.Options;
using ProgrammingResources.Library.DependencyInjection;
using ProgrammingResources.Library.Models;
using System.Data;
using System.Data.SqlClient;

namespace ProgrammingResources.Library.Services;
public class ExampleService
{
    private readonly ProgrammingResourcesOptions _options;

    public ExampleService(IOptions<ProgrammingResourcesOptions> options)
	{
        _options = options.Value;
    }

    public async Task<IEnumerable<Example>> GetAll()
    {
        using IDbConnection connection = new SqlConnection(_options.ConnectionString);

        var examples = await connection.QueryAsync<Example>("dbo.spExample_GetAll",
            commandType: CommandType.StoredProcedure);

        return examples;
    }

    public async Task<Example?> Get(int exampleId)
    {
        using IDbConnection connection = new SqlConnection(_options.ConnectionString);

        var example = await connection.QuerySingleOrDefaultAsync<Example>("dbo.spExample_Get",
            new { exampleId },
            commandType: CommandType.StoredProcedure);

        return example;
    }

    public async Task<Example> Save(Example example)
    {
        using IDbConnection connection = new SqlConnection(_options.ConnectionString);

        var output = await connection.QuerySingleAsync<Example>("dbo.spExample_Upsert",
            new
            {
                ExampleId = example.ExampleId,
                ResourceId = example.ResourceId,
                Text = example.Text,
                Url = example.Url,
                Page = example.Page,
                ProgrammingLanguageId = example.ProgrammingLanguageId,
                TypeId = example.TypeId,
                UserId = example.UserId,
            },
            commandType: CommandType.StoredProcedure);

        return output;
    }

    public async Task Delete(int exampleId)
    {
        using IDbConnection connection = new SqlConnection(_options.ConnectionString);

        await connection.ExecuteAsync("dbo.spExample_Delete",
            new { exampleId },
            commandType: CommandType.StoredProcedure);
    }
}
