using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using ProgrammingResources.Library.DependencyInjection;
using ProgrammingResources.Library.Models;
using ProgrammingResources.Library.Services.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace ProgrammingResources.Library.Services;

public class ProgrammingLanguageService : IProgrammingLanguageService
{
    private readonly ProgrammingResourcesOptions _options;
    private readonly IConfiguration _configuration;

    public ProgrammingLanguageService(IOptions<ProgrammingResourcesOptions> options,
        IConfiguration configuration)
    {
        _options = options.Value;
        _configuration = configuration;
    }

    public async Task<IEnumerable<ProgrammingLanguage>> GetAll()
    {
        string connectionString = _configuration.GetConnectionString(_options.ConnectionString);
        using IDbConnection connection = new SqlConnection(connectionString);

        var programmingLanguages = await connection.QueryAsync<ProgrammingLanguage>("dbo.spProgrammingLanguage_GetAll",
            commandType: CommandType.StoredProcedure);

        return programmingLanguages;
    }

    public async Task<ProgrammingLanguage?> Get(int ProgrammingLanguageId)
    {
        string connectionString = _configuration.GetConnectionString(_options.ConnectionString);
        using IDbConnection connection = new SqlConnection(connectionString);

        var programmingLanguage = (await connection.QueryAsync<ProgrammingLanguage>("dbo.spProgrammingLanguage_Get",
            new { ProgrammingLanguageId },
            commandType: CommandType.StoredProcedure))
            .SingleOrDefault();

        return programmingLanguage;
    }

    public async Task<ProgrammingLanguage> Add(ProgrammingLanguage language)
    {
        string connectionString = _configuration.GetConnectionString(_options.ConnectionString);
        using IDbConnection connection = new SqlConnection(connectionString);

        ProgrammingLanguage programmingLanguage = (await connection.QueryAsync<ProgrammingLanguage>("dbo.spProgrammingLanguage_Insert",
            new { UserId = language.UserId, Language = language.Language },
            commandType: CommandType.StoredProcedure))
            .Single();

        return programmingLanguage;
    }

    public async Task Delete(int programmingLanguageId)
    {
        string connectionString = _configuration.GetConnectionString(_options.ConnectionString);
        using IDbConnection connection = new SqlConnection(connectionString);

        await connection.ExecuteAsync("dbo.spProgrammingLanguage_Delete",
            new { programmingLanguageId },
            commandType: CommandType.StoredProcedure);
    }
}
