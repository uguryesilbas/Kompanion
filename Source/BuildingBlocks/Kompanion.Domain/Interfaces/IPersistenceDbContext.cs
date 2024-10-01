using Kompanion.Domain.Abstracts;
using MySql.Data.MySqlClient;

namespace Kompanion.Domain.Interfaces;

public interface IPersistenceDbContext : IAsyncDisposable
{
    Task<int> ExecuteStoredProcedureAsync(string storeProcedureName, CancellationToken cancellationToken = default, params MySqlParameter[] parameters);

    Task<T> FindByIdAsync<T>(string storeProcedureName, MySqlParameter parameter, CancellationToken cancellationToken = default) where T : BaseEntity, new();

    Task<bool> InsertAsync<T>(string storeProcedureName, T entity, CancellationToken cancellationToken = default) where T : BaseEntity;
}