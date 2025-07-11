﻿using BankMore.Auth.Domain.Entities;
using BankMore.Auth.Domain.Repositories;
using Dapper;
using System.Data;

namespace BankMore.Auth.Infrastructure.Repositories
{
    public class MovimentoRepositorySqlServer : IMovimentoRepository
    {
        private readonly IDbConnection _connection;

        public MovimentoRepositorySqlServer(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task AdicionarAsync(Movimento movimento)
        {
            var sql = @"INSERT INTO movimento (idmovimento, idcontacorrente, datamovimento, tipomovimento, valor)
                        VALUES (@Id, @IdContaCorrente, @DataMovimento, @Tipo, @Valor)";

            await _connection.ExecuteAsync(sql, movimento);
        }

        public Task<bool> ExisteIdempotenciaAsync(string chaveIdempotencia)
        {
            var sql = "SELECT COUNT(1) FROM movimento WHERE chave_idempotencia = @ChaveIdempotencia;";
            return _connection.ExecuteScalarAsync<bool>(sql, new { ChaveIdempotencia = chaveIdempotencia });
        }

        public async Task<decimal> CalcularSaldoAsync(Guid idConta)
        {
            const string sql = @"
        SELECT 
            COALESCE(SUM(CASE WHEN tipomovimento = 'C' THEN valor ELSE 0 END), 0) -
            COALESCE(SUM(CASE WHEN tipomovimento = 'D' THEN valor ELSE 0 END), 0)
        FROM movimento
        WHERE idcontacorrente = @Id";

            return await _connection.ExecuteScalarAsync<decimal>(sql, new { Id = idConta });
        }

    }
}