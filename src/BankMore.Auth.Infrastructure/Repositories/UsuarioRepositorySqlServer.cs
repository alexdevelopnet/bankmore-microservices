using BankMore.Auth.Domain.Entities;
using BankMore.Auth.Domain.Repositories;
using BankMore.Auth.Infrastructure.Mappers;
using Dapper;
using System.Data;

namespace BankMore.Auth.Infrastructure.Repositories
{
    public class UsuarioRepositorySqlServer : IUsuarioRepository
    {
        private readonly IDbConnection _connection;

        public UsuarioRepositorySqlServer(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<Usuario?> ObterPorCpfAsync(string cpf)
        {
            const string sql = "SELECT TOP 1 * FROM Usuarios WHERE Cpf = @Cpf;";
            var result = await _connection.QuerySingleOrDefaultAsync<UsuarioDTO>(sql, new { Cpf = cpf });
            return result?.ToEntity();
        }

        public async Task<Usuario?> ObterPorEmailAsync(string email)
        {
            const string sql = "SELECT TOP 1 * FROM Usuarios WHERE Email = @Email;";
            var result = await _connection.QuerySingleOrDefaultAsync<UsuarioDTO>(sql, new { Email = email });
            return result?.ToEntity();
        }

        public async Task<Usuario?> ObterPorIdAsync(Guid id)
        {
            const string sql = "SELECT TOP 1 * FROM Usuarios WHERE Id = @Id;";
            var result = await _connection.QuerySingleOrDefaultAsync<UsuarioDTO>(sql, new { Id = id });
            return result?.ToEntity();
        }

        public async Task AdicionarAsync(Usuario usuario)
        {
            const string sql = @"
                INSERT INTO Usuarios (Id, Nome, Cpf, Email, SenhaHash, CriadoEm)
                VALUES (@Id, @Nome, @Cpf, @Email, @Senha, GETDATE());";

            await _connection.ExecuteAsync(sql, new
            {
                usuario.Id,
                usuario.Nome,
                Cpf = usuario.Cpf.Numero,
                Email = usuario.Email.Endereco,
                Senha = usuario.SenhaHash
            });
        }

        public async Task AtualizarAsync(Usuario usuario)
        {
            const string sql = @"
                UPDATE Usuarios
                SET Nome = @Nome, Email = @Email, SenhaHash = @Senha
                WHERE Id = @Id;";

            await _connection.ExecuteAsync(sql, new
            {
                usuario.Id,
                usuario.Nome,
                Email = usuario.Email.Endereco,
                Senha = usuario.SenhaHash
            });
        }
    }
}
