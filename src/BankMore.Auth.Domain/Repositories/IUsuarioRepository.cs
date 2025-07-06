using BankMore.Auth.Domain.Entities;

namespace BankMore.Auth.Domain.Repositories
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> ObterPorCpfAsync(string cpf);
        Task<Usuario?> ObterPorEmailAsync(string email);
        Task<Usuario?> ObterPorIdAsync(Guid id);
        Task AdicionarAsync(Usuario usuario);
        Task AtualizarAsync(Usuario usuario);
    }
}