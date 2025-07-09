using BankMore.Auth.Domain.Entities;
using BankMore.Auth.Domain.Repositories;
using Dapper;
using System.Data;

public class ContaCorrenteRepositorySqlServer : IContaCorrenteRepository
{
    private readonly IDbConnection _connection;

    public ContaCorrenteRepositorySqlServer(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task AdicionarAsync(ContaCorrente conta)
    {
        var sql = @"INSERT INTO contacorrente (idcontacorrente, numero, nome, ativo, senha, salt)
                        VALUES (@Id, @Numero, @Nome, @Ativo, @Senha, @Salt)";

        await _connection.ExecuteAsync(sql, new
        {
            Id = conta.Id.ToString(),
            conta.Numero,
            conta.Nome,
            Ativo = conta.Ativo ? 1 : 0,
            conta.Senha,
            conta.Salt
        });
    }

    public async Task<bool> NumeroExisteAsync(int numero)
    {
        var sql = "SELECT COUNT(1) FROM contacorrente WHERE numero = @Numero";
        var count = await _connection.ExecuteScalarAsync<int>(sql, new { Numero = numero });
        return count > 0;
    }

    public async Task<ContaCorrente?> ObterPorIdAsync(Guid id)
    {
        var sql = @"SELECT * FROM contacorrente WHERE idcontacorrente = @Id";
        return await _connection.QueryFirstOrDefaultAsync<ContaCorrente>(sql, new { Id = id.ToString() });
    }

    public async Task<ContaCorrente?> ObterPorNumeroAsync(int numero)
    {
        var sql = @"SELECT * FROM contacorrente WHERE numero = @Numero";
        return await _connection.QueryFirstOrDefaultAsync<ContaCorrente>(sql, new { Numero = numero });
    }

    public async Task AtualizarSaldoAsync(Guid id, decimal novoSaldo)
    {
        var sql = @"UPDATE contacorrente SET saldo = @Saldo WHERE idcontacorrente = @Id";
        await _connection.ExecuteAsync(sql, new { Id = id.ToString(), Saldo = novoSaldo });
    }

    public async Task<bool> ContaEstaAtivaAsync(Guid id)
    {
        var sql = @"SELECT ativo FROM contacorrente WHERE idcontacorrente = @Id";
        var ativo = await _connection.ExecuteScalarAsync<int?>(sql, new { Id = id.ToString() });
        return ativo == 1;
    }
}
