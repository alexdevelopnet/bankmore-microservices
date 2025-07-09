using BankMore.Auth.Domain.Entities;
using BankMore.Auth.Domain.Repositories;
using MediatR;

namespace BankMore.Auth.Application.Commands
{
    internal class RealizarTransferenciaCommandHandler : IRequestHandler<RealizarTransferenciaCommand, Guid>
    {
        private readonly IMovimentoRepository _movimentoRepo;
        private readonly ITransferenciaRepository _transferenciaRepo;

        public RealizarTransferenciaCommandHandler(IMovimentoRepository movimentoRepo, ITransferenciaRepository transferenciaRepo)
        {
            _movimentoRepo = movimentoRepo;
            _transferenciaRepo = transferenciaRepo;
        }

        public async Task<Guid> Handle(RealizarTransferenciaCommand request, CancellationToken cancellationToken)
        {
            var data = DateTime.Now;

            // Generate a unique idempotency key for each transaction
            var chaveIdempotenciaDebito = Guid.NewGuid().ToString();
            var chaveIdempotenciaCredito = Guid.NewGuid().ToString();

            var debito = new Movimento(Guid.NewGuid(), request.IdContaOrigem, data, "D", request.Valor, chaveIdempotenciaDebito);
            var credito = new Movimento(Guid.NewGuid(), request.IdContaDestino, data, "C", request.Valor, chaveIdempotenciaCredito);

            await _movimentoRepo.AdicionarAsync(debito);
            await _movimentoRepo.AdicionarAsync(credito);

            var transferencia = new Transferencia(Guid.NewGuid(), request.IdContaOrigem, request.IdContaDestino, data, request.Valor);
            await _transferenciaRepo.AdicionarAsync(transferencia);

            return transferencia.Id;
        }
    }
}
