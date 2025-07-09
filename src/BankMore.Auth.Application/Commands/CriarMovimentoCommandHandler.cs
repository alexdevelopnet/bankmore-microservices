using BankMore.Auth.Domain.Entities;
using BankMore.Auth.Domain.Repositories;
using MediatR;

namespace BankMore.Auth.Application.Commands
{
    public class CriarMovimentoCommandHandler : IRequestHandler<CriarMovimentoCommand, Guid>
    {
        private readonly IMovimentoRepository _repository;

        public CriarMovimentoCommandHandler(IMovimentoRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(CriarMovimentoCommand request, CancellationToken cancellationToken)
        {
            if (request.Tipo != 'C' && request.Tipo != 'D')
                throw new ArgumentException("Tipo de movimento inválido. Use 'C' (crédito) ou 'D' (débito).");

            var movimento = new Movimento(
                Guid.NewGuid(),
                request.IdContaCorrente,
                DateTime.Now,
                request.Tipo,
                request.Valor
            );

            await _repository.AdicionarAsync(movimento);

            return movimento.Id;
        }
    }
}
