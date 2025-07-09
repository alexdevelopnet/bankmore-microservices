using BankMore.Auth.Domain.Entities;
using BankMore.Auth.Domain.Repositories;
using MediatR;

namespace BankMore.Auth.Application.Commands
{
    internal class RealizarTransferenciaCommandHandler : IRequestHandler<RealizarTransferenciaCommand, Guid>
    {
        private readonly IMovimentoRepository _movimentoRepo;
        private readonly ITransferenciaRepository _transferenciaRepo;
        private readonly IIdempotenciaRepository _idempotenciaRepo;

        public RealizarTransferenciaCommandHandler(IMovimentoRepository movimentoRepo, ITransferenciaRepository transferenciaRepo, IIdempotenciaRepository idempotenciaRepo)
        {
            _movimentoRepo = movimentoRepo;
            _transferenciaRepo = transferenciaRepo;
            _idempotenciaRepo = idempotenciaRepo;
        }

        public async Task<Guid> Handle(RealizarTransferenciaCommand request, CancellationToken cancellationToken)
        {
            if (await _idempotenciaRepo.ExisteAsync(request.ChaveIdempotencia))
            {
                return Guid.Empty;
            } 
            var data = DateTime.Now;
             
            var chaveIdempotenciaDebito = Guid.NewGuid().ToString();
            var chaveIdempotenciaCredito = Guid.NewGuid().ToString();

            var debito = new Movimento(Guid.NewGuid(), request.IdContaOrigem, data, "D", request.Valor, chaveIdempotenciaDebito);
            var credito = new Movimento(Guid.NewGuid(), request.IdContaDestino, data, "C", request.Valor, chaveIdempotenciaCredito);

            await _movimentoRepo.AdicionarAsync(debito);
            await _movimentoRepo.AdicionarAsync(credito);

            var transferencia = new Transferencia(Guid.NewGuid(), request.IdContaOrigem, request.IdContaDestino, data, request.Valor);
            await _transferenciaRepo.AdicionarAsync(transferencia);

            await _idempotenciaRepo.SalvarAsync(request.ChaveIdempotencia, transferencia.Id.ToString());

            return transferencia.Id;
        }
    }
}
