namespace BankMore.Auth.Domain.Repositories
{
    public interface IIdempotenciaRepository
    {
        Task<bool> ExisteAsync(string chaveIdempotencia);
        Task SalvarAsync(string chaveIdempotencia, string resultado);
    }
}
