using BankMore.Auth.Domain.Entities;
using BankMore.Auth.Domain.Repositories;
using Dapper;
using System.Data;

namespace BankMore.Auth.Infrastructure.Repositories
{
    public class TransferenciaRepositoryMySql : ITransferenciaRepository
    {
        private readonly IDbConnection _connection;

        public TransferenciaRepositoryMySql(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task AdicionarAsync(Transferencia transferencia)
        {
            const string sql = @"INSERT INTO transferencia (
                idtransferencia,
                idcontacorrente_origem,
                idcontacorrente_destino,
                datamovimento,
                valor
            ) VALUES (
                @Id,
                @IdContaOrigem,
                @IdContaDestino,
                @DataMovimento,
                @Valor
            );";

            await _connection.ExecuteAsync(sql, new
            {
                transferencia.Id,
                transferencia.IdContaOrigem,
                transferencia.IdContaDestino,
                DataMovimento = transferencia.DataMovimento.ToString("yyyy-MM-dd HH:mm:ss"),
                transferencia.Valor
            });
        }
    }
}
