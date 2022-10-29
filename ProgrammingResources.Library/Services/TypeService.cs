using Microsoft.Extensions.Configuration;
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
    private readonly IConfiguration _configuration;

    public TypeService(IOptions<ProgrammingResourcesOptions> options,
        IConfiguration configuration)
    {
        _options = options.Value;
        _configuration = configuration;
    }

    public async Task<IEnumerable<Type>> GetAll()
    {
        string connectionString = _configuration.GetConnectionString(_options.ConnectionString);
        using IDbConnection connection = new SqlConnection(connectionString);

        var types = await connection.QueryAsync<Type>("dbo.spType_GetAll",
            commandType: CommandType.StoredProcedure);

        return types;
    }

    public async Task<Type?> Get(int typeId)
    {
        string connectionString = _configuration.GetConnectionString(_options.ConnectionString);
        using IDbConnection connection = new SqlConnection(connectionString);

        var type = (await connection.QueryAsync<Type>("dbo.spType_Get",
            new { typeId },
            commandType: CommandType.StoredProcedure))
            .SingleOrDefault();

        return type;
    }

    public async Task<Type> Add(Type type)
    {
        string connectionString = _configuration.GetConnectionString(_options.ConnectionString);
        using IDbConnection connection = new SqlConnection(connectionString);

        Type output = (await connection.QueryAsync<Type>("dbo.spType_Insert",
            new { UserId = type.UserId, Name = type.Name },
            commandType: CommandType.StoredProcedure))
            .Single();

        return output;
    }

    public async Task Delete(int typeId)
    {
        string connectionString = _configuration.GetConnectionString(_options.ConnectionString);
        using IDbConnection connection = new SqlConnection(connectionString);

        await connection.ExecuteAsync("dbo.spType_Delete",
            new { typeId },
            commandType: CommandType.StoredProcedure);
    }
}
