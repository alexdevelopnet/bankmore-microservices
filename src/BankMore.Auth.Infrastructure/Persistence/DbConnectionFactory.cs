using BankMore.Auth.Domain.Abstractions;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace BankMore.Auth.Infrastructure.Persistence
{

    public class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly IConfiguration _configuration;
        private readonly bool _isDocker;

        public DbConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
            _isDocker = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true";
        }

        public IDbConnection CreateConnection()
        { 
            if (_isDocker)
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                var connection = new SqliteConnection(connectionString);
                connection.Open();
                return connection;
            }
            else
            {
                var connectionString = _configuration.GetConnectionString("SqlServer");
                var connection = new SqlConnection(connectionString);
                connection.Open();
                return connection;
            }
        }
    }


}
