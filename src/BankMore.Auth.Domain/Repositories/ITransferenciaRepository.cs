using BankMore.Auth.Domain.Entities;

namespace BankMore.Auth.Domain.Repositories
{
    public interface ITransferenciaRepository
    {
        Task AdicionarAsync(Transferencia transferencia);
    }
}
