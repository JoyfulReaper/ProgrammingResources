using Microsoft.Extensions.Options;
using ProgrammingResources.Library.DependencyInjection;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using Type = ProgrammingResources.Library.Models.Type;
using ProgrammingResources.Library.Services.Interfaces;

namespace ProgrammingResources.Library.Services;
public class TypeService : ITypeService
{
    private readonly ProgrammingResourcesOptions _options;

    public TypeService(IOptions<ProgrammingResourcesOptions> options)
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

    public async Task<Type?> Get(int typeId)
    {
        using IDbConnection connection = new SqlConnection(_options.ConnectionString);

        var type = (await connection.QueryAsync<Type>("dbo.spType_Get",
            new { typeId },
            commandType: CommandType.StoredProcedure))
            .SingleOrDefault();

        return type;
    }

    public async Task<Type> Add(Type type)
    {
        using IDbConnection connection = new SqlConnection(_options.ConnectionString);

        Type output = (await connection.QueryAsync<Type>("dbo.spType_Insert",
            new { UserId = type.UserId, Name = type.Name },
            commandType: CommandType.StoredProcedure))
            .Single();

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
