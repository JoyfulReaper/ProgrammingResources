namespace ProgrammingResourceLibrary.DataAccess;

public interface IDataAccess
{
    void CommitTransaction();
    void Dispose();
    string GetConnectionString(string name);
    Task<IEnumerable<T>> LoadDataAsync<T, U>(string storedProcedure, U parameters, string connectionStringName);
    Task<IEnumerable<T>> LoadDataInTransactionAsync<T, U>(string storedProcedure, U parameters);
    void RollbackTransaction();
    Task SaveDataAsync<T>(string storedProcedure, T parameters, string connectionStringName);
    Task SaveDataInTransactionAsync<T>(string storedProcedure, T parameters);
    Task<int> SaveDataAndGetIdAsync<T>(string storedProcedure, T parameters, string connectionStringName);
    void StartTransaction(string connectionStringName);

    Task<IEnumerable<T>> QueryRawSql<T, U>(string sql, U parameters, string connectionStringName);
    Task ExecuteRawSql<T>(string sql, T parameters, string connectionStringName);
}
