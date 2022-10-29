using Dapper;
using Microsoft.Extensions.Options;
using ProgrammingResources.Library.DependencyInjection;
using ProgrammingResources.Library.Models;
using ProgrammingResources.Library.Services.Repos;
using System.Data;
using System.Data.SqlClient;

namespace ProgrammingResources.Library.Services;

public class ProgrammingLanguageRepo : IProgrammingLanguageRepo
{
    private readonly ProgrammingResourcesOptions _options;

    public ProgrammingLanguageRepo(IOptions<ProgrammingResourcesOptions> options)
    {
        _options = options.Value;
    }

    public async Task<IEnumerable<ProgrammingLanguage>> GetAll()
    {
        using IDbConnection connection = new SqlConnection(_options.ConnectionString);

        var programmingLanguages = await connection.QueryAsync<ProgrammingLanguage>("dbo.spProgrammingLanguage_GetAll",
            commandType: CommandType.StoredProcedure);

        return programmingLanguages;
    }

    public async Task<ProgrammingLanguage?> Get(string language)
    {
        using IDbConnection connection = new SqlConnection(_options.ConnectionString);

        var lang = await connection.QuerySingleOrDefaultAsync<ProgrammingLanguage>("dbo.spProgrammingLanguage_GetByName",
            new { language },
            commandType: CommandType.StoredProcedure);

        return lang;
    }

    public async Task<ProgrammingLanguage?> Get(int ProgrammingLanguageId)
    {
        using IDbConnection connection = new SqlConnection(_options.ConnectionString);

        var programmingLanguage = await connection.QuerySingleOrDefaultAsync<ProgrammingLanguage>("dbo.spProgrammingLanguage_Get",
            new { ProgrammingLanguageId },
            commandType: CommandType.StoredProcedure);

        return programmingLanguage;
    }

    public async Task<ProgrammingLanguage> Add(ProgrammingLanguage language)
    {
        using IDbConnection connection = new SqlConnection(_options.ConnectionString);

        ProgrammingLanguage programmingLanguage = (await connection.QuerySingleAsync<ProgrammingLanguage>("dbo.spProgrammingLanguage_Insert",
            new { UserId = language.UserId, Language = language.Language },
            commandType: CommandType.StoredProcedure));

        language.ProgrammingLanguageId = programmingLanguage.ProgrammingLanguageId;

        return programmingLanguage;
    }

    public async Task Delete(int programmingLanguageId)
    {
        using IDbConnection connection = new SqlConnection(_options.ConnectionString);

        await connection.ExecuteAsync("dbo.spProgrammingLanguage_Delete",
            new { programmingLanguageId },
            commandType: CommandType.StoredProcedure);
    }
}
