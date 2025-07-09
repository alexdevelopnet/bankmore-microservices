using BankMore.Auth.Domain.Repositories;
using Dapper;
using System.Data;
using System.Data.Common;

namespace BankMore.Auth.Infrastructure.Repositories
{
    public class ContaCorrenteRepositoryMySql : IContaCorrenteRepository
    {
        private readonly IDbConnection _connection;

        public ContaCorrenteRepositoryMySql(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task AdicionarAsync(Domain.Entities.ContaCorrente conta)
        {
            var sql = @"INSERT INTO contacorrente 
                        (idcontacorrente, numero, nome, ativo, senha, salt)
                        VALUES (@Id, @Numero, @Nome, @Ativo, @Senha, @Salt)";

            await _connection.ExecuteAsync(sql, new
            {
                Id = conta.Id,
                conta.Numero,
                conta.Nome,
                Ativo = conta.Ativo ? 1 : 0,
                conta.Senha,
                conta.Salt
            });
        }

        public async Task<bool> NumeroExisteAsync(int numero)
        {
            var sql = @"SELECT COUNT(*) FROM contacorrente WHERE numero = @Numero";
            var count = await _connection.ExecuteScalarAsync<int>(sql, new { Numero = numero });
            return count > 0;
        }

        public async Task<Domain.Entities.ContaCorrente?> ObterPorIdAsync(Guid id)
        {
            var sql = @"SELECT * FROM contacorrente WHERE idcontacorrente = @Id";
            return await _connection.QueryFirstOrDefaultAsync<Domain.Entities.ContaCorrente>(sql, new { Id = id });
        }

        public async Task<Domain.Entities.ContaCorrente?> ObterPorNumeroAsync(int numero)
        {
            var sql = @"SELECT * FROM contacorrente WHERE numero = @Numero";
            return await _connection.QueryFirstOrDefaultAsync<Domain.Entities.ContaCorrente>(sql, new { Numero = numero });
        }

        public async Task AtualizarSaldoAsync(Guid idContaCorrente, decimal novoSaldo)
        {
            // Aqui assumimos que existe uma coluna "saldo" (verifique isso no seu modelo e banco)
            var sql = @"UPDATE contacorrente SET saldo = @Saldo WHERE idcontacorrente = @Id";
            await _connection.ExecuteAsync(sql, new { Id = idContaCorrente, Saldo = novoSaldo });
        }

        public async Task<bool> ContaEstaAtivaAsync(Guid idContaCorrente)
        {
            var sql = @"SELECT ativo FROM contacorrente WHERE idcontacorrente = @Id";
            var ativo = await _connection.ExecuteScalarAsync<int?>(sql, new { Id = idContaCorrente });
            return ativo == 1;
        }

        public async Task<decimal> ObterSaldoAsync(Guid idConta)
        {
            var sql = @"
        SELECT 
            COALESCE(SUM(CASE 
                WHEN tipomovimento = 'C' THEN valor 
                WHEN tipomovimento = 'D' THEN -valor 
                ELSE 0 END), 0)
        FROM movimento
        WHERE idcontacorrente = @id";

            return await _connection.ExecuteScalarAsync<decimal>(sql, new { id = idConta });
        }

    }
}
