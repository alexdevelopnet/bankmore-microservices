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

        public Task AdicionarAsync(Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public Task AtualizarAsync(Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public async Task<Usuario?> ObterPorCpfAsync(string cpf)
        {
            const string sql = "SELECT * FROM Usuarios WHERE Cpf = @Cpf LIMIT 1;";
            var result = await _connection.QuerySingleOrDefaultAsync<UsuarioDTO>(sql, new { Cpf = cpf });
            return result?.ToEntity();
        }

        public Task<Usuario?> ObterPorEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<Usuario?> ObterPorIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        // os demais métodos iguais, com sintaxe MySQL
    }
}