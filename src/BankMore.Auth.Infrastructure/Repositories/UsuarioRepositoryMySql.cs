using BankMore.Auth.Domain.Entities;
using BankMore.Auth.Domain.Repositories;
using BankMore.Auth.Infrastructure.Mappers;
using Dapper;
using System.Data;

namespace BankMore.Auth.Infrastructure.Repositories
{
    public class UsuarioRepositoryMySql : IUsuarioRepository
    {
        private readonly IDbConnection _connection;

        public UsuarioRepositoryMySql(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task AdicionarAsync(ContaCorrente conta)
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

        public async Task<ContaCorrente?> ObterPorIdAsync(Guid id)
        {
            var sql = @"SELECT * FROM contacorrente WHERE idcontacorrente = @Id";
            return await _connection.QueryFirstOrDefaultAsync<ContaCorrente>(sql, new { Id = id });
        }

        public async Task<ContaCorrente?> ObterPorNumeroAsync(int numero)
        {
            var sql = @"SELECT * FROM contacorrente WHERE numero = @Numero";
            return await _connection.QueryFirstOrDefaultAsync<ContaCorrente>(sql, new { Numero = numero });
        }

        public async Task AtualizarSaldoAsync(Guid idContaCorrente, decimal novoSaldo)
        {
            // Aqui assumimos que existe uma coluna "saldo" (verifique isso no seu modelo e banco)
            var sql = @"UPDATE contacorrente SET saldo = @Saldo WHERE idcontacorrente = @Id";
            await _connection.ExecuteAsync(sql, new { Id = idContaCorrente, Saldo = novoSaldo });
        }

        public async Task<bool> ContaAtivaAsync(Guid idContaCorrente)
        {
            var sql = @"SELECT ativo FROM contacorrente WHERE idcontacorrente = @Id";
            var ativo = await _connection.ExecuteScalarAsync<int?>(sql, new { Id = idContaCorrente });
            return ativo == 1;
        }

        public Task<Usuario?> ObterPorCpfAsync(string cpf)
        {
            throw new NotImplementedException();
        }

        public Task<Usuario?> ObterPorEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        Task<Usuario?> IUsuarioRepository.ObterPorIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task AdicionarAsync(Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public Task AtualizarAsync(Usuario usuario)
        {
            throw new NotImplementedException();
        }
    }
}