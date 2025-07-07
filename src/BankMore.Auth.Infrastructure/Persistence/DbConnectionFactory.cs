using BankMore.Auth.Domain.Abstractions;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
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
                return new MySqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            }
            else
            {
                return new SqlConnection(_configuration.GetConnectionString("SqlServer"));
            }
        }
    }


}
