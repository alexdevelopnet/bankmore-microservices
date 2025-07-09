using BankMore.Auth.Domain.Entities;

namespace BankMore.Auth.Domain.Repositories
{
    public interface IMovimentoRepository
    {
        Task AdicionarAsync(Movimento movimento);
        Task<decimal> CalcularSaldoAsync(Guid contaId);
        Task<bool> ExisteIdempotenciaAsync(string chaveIdempotencia);
        

    }
}
