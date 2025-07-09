using BankMore.Auth.Domain.Entities;

namespace BankMore.Auth.Domain.Repositories
{
    public interface IContaCorrenteRepository
    {
        Task AdicionarAsync(ContaCorrente conta);
        Task<bool> NumeroExisteAsync(int numero);
        Task<ContaCorrente?> ObterPorIdAsync(Guid id);
        Task<ContaCorrente?> ObterPorNumeroAsync(int numero);
        Task AtualizarSaldoAsync(Guid idConta, decimal novoSaldo);
        Task<bool> ContaEstaAtivaAsync(Guid idConta);
    }
}
