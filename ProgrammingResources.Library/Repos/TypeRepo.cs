using Microsoft.Extensions.Options;
using ProgrammingResources.Library.DependencyInjection;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using Type = ProgrammingResources.Library.Models.Type;
using ProgrammingResources.Library.Services.Repos;

namespace ProgrammingResources.Library.Services;
public class TypeRepo : ITypeRepo
{
    private readonly ProgrammingResourcesOptions _options;

    public TypeRepo(IOptions<ProgrammingResourcesOptions> options)
    {
        _options = options.Value;
    }

    public async Task<IEnumerable<Type>> GetAll()
    {
        using IDbConnection connection = new SqlConnection(_options.ConnectionString);

        var types = await connection.QueryAsync<Type>("dbo.spType_GetAll",
            commandType: CommandType.StoredProcedure);

        return types;
    }

    public async Task<Type?> Get(string type)
    {
        using IDbConnection connection = new SqlConnection(_options.ConnectionString);

        var output = await connection.QuerySingleOrDefaultAsync<Type>("dbo.spType_GetByName",
            new { type },
            commandType: CommandType.StoredProcedure);

        return output;
    }

    public async Task<Type?> Get(int typeId)
    {
        using IDbConnection connection = new SqlConnection(_options.ConnectionString);

        var type = await connection.QuerySingleOrDefaultAsync<Type>("dbo.spType_Get",
            new { typeId },
            commandType: CommandType.StoredProcedure);

        return type;
    }

    public async Task<Type> Add(Type type)
    {
        using IDbConnection connection = new SqlConnection(_options.ConnectionString);

        Type output = await connection.QuerySingleAsync<Type>("dbo.spType_Insert",
            new { UserId = type.UserId, Name = type.Name },
            commandType: CommandType.StoredProcedure);

        type.TypeId = output.TypeId;

        return output;
    }

    public async Task Delete(int typeId)
    {
        using IDbConnection connection = new SqlConnection(_options.ConnectionString);

        await connection.ExecuteAsync("dbo.spType_Delete",
            new { typeId },
            commandType: CommandType.StoredProcedure);
    }
}
