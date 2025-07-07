using BankMore.Auth.Domain.Abstractions;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace BankMore.Auth.Infrastructure.Persistence
{

    using Microsoft.Data.SqlClient;

    public class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DbConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("SqlServer");
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }



}
