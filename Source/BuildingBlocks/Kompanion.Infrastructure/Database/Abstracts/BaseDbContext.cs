using Kompanion.Application.Extensions;
using Kompanion.Domain.Abstracts;
using Kompanion.Domain.Extensions;
using Kompanion.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.Common;
using System.Reflection;

namespace Kompanion.Infrastructure.Database.Abstracts;

public abstract class BaseDbContext : IPersistenceDbContext
{
    private readonly string _connectionString;
    private readonly IHttpContextAccessor _httpContextAccessor;

    private MySqlConnection _connection;

    protected BaseDbContext(IServiceProvider serviceProvider, string connectionStringSectionName)
    {
        _connectionString = serviceProvider.GetRequiredService<IConfiguration>().GetConnectionString(connectionStringSectionName);
        _httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
    }

    public virtual MySqlCommand CreateStoredProcedureCommand(string storeProcedureName, MySqlConnection connection) => new MySqlCommand(storeProcedureName, connection) { CommandType = CommandType.StoredProcedure };

    public virtual async Task<MySqlConnection> GetConnectionAsync(CancellationToken cancellationToken)
    {
        if (_connection is null)
        {
            _connection = new MySqlConnection(_connectionString);

            await _connection.OpenAsync(cancellationToken);
        }

        return _connection;
    }

    public virtual async ValueTask DisposeAsync()
    {
        if (_connection is not null)
        {
            await _connection.CloseAsync();
            await _connection.DisposeAsync();
            _connection = null;
        }
    }

    public virtual async Task<int> ExecuteStoredProcedureAsync(string storeProcedureName, CancellationToken cancellationToken = default, params MySqlParameter[] parameters)
    {
        using MySqlConnection connection = await GetConnectionAsync(cancellationToken);

        using MySqlCommand command = CreateStoredProcedureCommand(storeProcedureName, connection);

        command.Parameters.AddRange(parameters);

        return await command.ExecuteNonQueryAsync(cancellationToken);
    }

    public virtual async Task<T> FindByIdAsync<T>(string storeProcedureName, MySqlParameter parameter, CancellationToken cancellationToken = default) where T : BaseEntity, new()
    {
        T entity = new();

        using MySqlConnection connection = await GetConnectionAsync(cancellationToken);

        using MySqlCommand command = CreateStoredProcedureCommand(storeProcedureName, connection);

        command.Parameters.Add(parameter);

        using (DbDataReader reader = await command.ExecuteReaderAsync(cancellationToken))
        {
            if (!reader.HasRows)
                return null;

            while (await reader.ReadAsync(cancellationToken))
            {
                foreach (PropertyInfo prop in typeof(T).GetProperties())
                {
                    if (!reader.IsDBNull(reader.GetOrdinal(prop.Name)))
                    {
                        prop.SetValue(entity, reader[prop.Name]);
                    }
                }
            }
        }

        return entity;
    }

    public virtual async Task<bool> InsertAsync<T>(string storeProcedureName, T entity, CancellationToken cancellationToken = default) where T : BaseEntity
    {
        using MySqlConnection connection = await GetConnectionAsync(cancellationToken);

        using MySqlCommand command = CreateStoredProcedureCommand(storeProcedureName, connection);

        AddEntityParameters(command, entity);

        MySqlParameter outputIdParam = new("p_LastInsertID", MySqlDbType.Int32)
        {
            Direction = ParameterDirection.Output,
        };

        command.Parameters.Add(outputIdParam);

        int result = await command.ExecuteNonQueryAsync(cancellationToken);

        if (result > 0)
        {
            typeof(T).GetProperty(nameof(BaseEntity.Id)).SetValue(entity, (int)outputIdParam.Value, null);

            return true;
        }

        return false;
    }

    private void AddEntityParameters<T>(MySqlCommand command, T entity) where T : BaseEntity
    {
        int userId = _httpContextAccessor.GetUserId();

        foreach (PropertyInfo prop in typeof(T).GetProperties())
        {
            if (prop.Name == nameof(BaseEntity.Id)) continue;

            object value = prop.Name switch
            {
                nameof(ITrackableEntity.CreatedDateTime) => DateTimeExtensions.Now,
                nameof(ITrackableEntity.CreatedUserId) => userId,
                _ => prop.GetValue(entity, null)
            };

            command.Parameters.AddWithValue($"p_{prop.Name}", value);
        }
    }
}