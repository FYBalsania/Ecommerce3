using System.Data;
using Npgsql;

namespace Ecommerce3.Infrastructure.Data;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}

internal sealed class DbConnectionFactory(string connectionString) : IDbConnectionFactory
{
    public IDbConnection CreateConnection()
        => new NpgsqlConnection(connectionString);
}
