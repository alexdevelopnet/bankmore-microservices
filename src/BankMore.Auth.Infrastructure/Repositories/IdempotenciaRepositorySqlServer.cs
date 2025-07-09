using BankMore.Auth.Domain.Repositories;
using Dapper;
using System.Data;

namespace BankMore.Auth.Infrastructure.Repositories
{
    public class IdempotenciaRepositorySqlServer : IIdempotenciaRepository
    {
        private readonly IDbConnection _connection;

        public IdempotenciaRepositorySqlServer(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<bool> ExisteAsync(string chaveIdempotencia)
        {
            const string sql = "SELECT COUNT(1) FROM idempotencia WHERE chave_idempotencia = @ChaveIdempotencia";
            var count = await _connection.ExecuteScalarAsync<int>(sql, new { ChaveIdempotencia = chaveIdempotencia });
            return count > 0;
        }

        public async Task SalvarAsync(string chaveIdempotencia, string resultado)
        {
            const string sql = @"INSERT INTO idempotencia (chave_idempotencia, requisicao, resultado) 
                                 VALUES (@ChaveIdempotencia, @Requisicao, @Resultado)";
            await _connection.ExecuteAsync(sql, new
            {
                ChaveIdempotencia = chaveIdempotencia,
                Requisicao = "",  
                Resultado = resultado
            });
        }
    }
}
