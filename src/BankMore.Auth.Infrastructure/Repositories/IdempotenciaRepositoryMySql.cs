using BankMore.Auth.Domain.Repositories;
using Dapper;
using System.Data;

namespace BankMore.Auth.Infrastructure.Repositories
{
    public class IdempotenciaRepositoryMySql : IIdempotenciaRepository
    {
        private readonly IDbConnection _connection;

        public IdempotenciaRepositoryMySql(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<bool> ExisteAsync(string chaveIdempotencia)
        {
            var sql = "SELECT COUNT(1) FROM idempotencia WHERE chave_idempotencia = @ChaveIdempotencia";
            var count = await _connection.ExecuteScalarAsync<int>(sql, new { ChaveIdempotencia = chaveIdempotencia });
            return count > 0;
        }

        public async Task SalvarAsync(string chaveIdempotencia, string resultado)
        {
            var sql = "INSERT INTO idempotencia (chave_idempotencia, requisicao, resultado) VALUES (@ChaveIdempotencia, @Requisicao, @Resultado)";
            await _connection.ExecuteAsync(sql, new { ChaveIdempotencia = chaveIdempotencia, Requisicao = "", Resultado = resultado });
        }
    }
}
