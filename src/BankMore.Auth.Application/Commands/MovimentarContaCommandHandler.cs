using BankMore.Auth.Domain.Entities;
using BankMore.Auth.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace BankMore.Auth.Application.Commands
{
    public class MovimentarContaCommandHandler : IRequestHandler<MovimentarContaCommand, Unit>
    {
        private readonly IContaCorrenteRepository _contaRepo;
        private readonly IMovimentoRepository _movimentoRepo;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MovimentarContaCommandHandler(
            IContaCorrenteRepository contaRepo,
            IMovimentoRepository movimentoRepo,
            IHttpContextAccessor httpContextAccessor)
        {
            _contaRepo = contaRepo;
            _movimentoRepo = movimentoRepo;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Unit> Handle(MovimentarContaCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst("id")?.Value;
            if (userId is null)
                throw new UnauthorizedAccessException("TOKEN_INVALID");

            var idContaLogada = Guid.Parse(userId);

            var idempotenteExiste = await _movimentoRepo.ExisteIdempotenciaAsync(request.ChaveIdempotencia);
            if (idempotenteExiste)
                return Unit.Value; // Requisição repetida, já registrada.

            var conta = request.NumeroConta.HasValue
                ? await _contaRepo.ObterPorNumeroAsync(request.NumeroConta.Value)
                : await _contaRepo.ObterPorIdAsync(idContaLogada);

            if (conta is null)
                throw new InvalidOperationException("INVALID_ACCOUNT");

            if (!conta.Ativo)
                throw new InvalidOperationException("INACTIVE_ACCOUNT");

            if (request.Valor <= 0)
                throw new InvalidOperationException("INVALID_VALUE");

            if (request.Tipo != "C" && request.Tipo != "D")
                throw new InvalidOperationException("INVALID_TYPE");

            if (request.NumeroConta.HasValue && request.Tipo == "D" && conta.Id != idContaLogada)
                throw new InvalidOperationException("INVALID_TYPE");

            var movimento = new Movimento(
                Guid.NewGuid(),
                conta.Id,
                DateTime.UtcNow,
                request.Tipo,
                request.Valor,
                request.ChaveIdempotencia
            );

            await _movimentoRepo.AdicionarAsync(movimento);
            return Unit.Value;
        }
    }
}
