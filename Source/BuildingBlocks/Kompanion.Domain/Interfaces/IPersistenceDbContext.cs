using Kompanion.Domain.Abstracts;
using MySql.Data.MySqlClient;

namespace Kompanion.Domain.Interfaces;

public interface IPersistenceDbContext : IAsyncDisposable
{
    Task<MySqlConnection> GetConnectionAsync(CancellationToken cancellationToken);

    MySqlCommand CreateStoredProcedureCommand(string storeProcedureName, MySqlConnection connection);

    Task<int> ExecuteStoredProcedureAsync(string storeProcedureName, CancellationToken cancellationToken = default, params MySqlParameter[] parameters);

    Task<T> FindByIdAsync<T>(string storeProcedureName, int id, CancellationToken cancellationToken = default) where T : BaseEntity, new();

    Task<bool> InsertAsync<T>(string storeProcedureName, T entity, CancellationToken cancellationToken = default) where T : BaseEntity;

    Task<bool> UpdateAsync<T>(string storeProcedureName, T entity, CancellationToken cancellationToken = default) where T : BaseEntity;

    Task<bool> DeleteAsync(string storeProcedureName, int id, CancellationToken cancellationToken = default);
}