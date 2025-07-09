using BankMore.Auth.Domain.Entities;
using BankMore.Auth.Domain.Repositories;

public class ContaCorrenteRepositoryBase : IContaCorrenteRepository
{
    // métodos já implementados aqui...

    public virtual Task<ContaCorrente?> ObterPorNumeroAsync(int numero)
    {
        throw new NotImplementedException();
    }

    public virtual Task AtualizarSaldoAsync(Guid id, decimal valor, string tipoMovimento)
    {
        throw new NotImplementedException();
    }

    public virtual Task<bool> ContaEstaAtivaAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task AdicionarAsync(ContaCorrente conta)
    {
        throw new NotImplementedException();
    }

    public Task<bool> NumeroExisteAsync(int numero)
    {
        throw new NotImplementedException();
    }

    public Task<ContaCorrente?> ObterPorIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task AtualizarSaldoAsync(Guid idConta, decimal novoSaldo)
    {
        throw new NotImplementedException();
    }

    public Task<decimal> ObterSaldoAsync(Guid idConta)
    {
        throw new NotImplementedException();
    }

    public Task AtualizarAsync(object conta)
    {
        throw new NotImplementedException();
    }

    public Task<ContaCorrente?> ObterPorDocumentoOuNumeroAsync(string documentoOuNumero)
    {
        throw new NotImplementedException();
    }
}
