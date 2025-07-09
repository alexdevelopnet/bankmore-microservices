using BankMore.Auth.Domain.Entities;
using BankMore.Auth.Domain.Repositories;
using Dapper;
using System.Data;

namespace BankMore.Auth.Infrastructure.Repositories
{
    public class TransferenciaRepositorySqlServer : ITransferenciaRepository
    {
        private readonly IDbConnection _connection;

        public  TransferenciaRepositorySqlServer(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task AdicionarAsync(Transferencia transferencia)
        {
            var sql = @"INSERT INTO transferencia (
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

            await _connection.ExecuteAsync(sql, transferencia);
        }
    }
}
 
