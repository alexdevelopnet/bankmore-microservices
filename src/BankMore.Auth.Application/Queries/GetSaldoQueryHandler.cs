using BankMore.Auth.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace BankMore.Auth.Application.Queries
{
    public class GetSaldoQueryHandler : IRequestHandler<GetSaldoQuery, SaldoResult>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IContaCorrenteRepository _contaRepo;
        private readonly IMovimentoRepository _movimentoRepo;

        public GetSaldoQueryHandler(
            IHttpContextAccessor httpContextAccessor,
            IContaCorrenteRepository contaRepo,
            IMovimentoRepository movimentoRepo)
        {
            _httpContextAccessor = httpContextAccessor;
            _contaRepo = contaRepo;
            _movimentoRepo = movimentoRepo;
        }

        public async Task<SaldoResult> Handle(GetSaldoQuery request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst("id")?.Value;

            if (userId == null || !Guid.TryParse(userId, out var contaId))
                throw new UnauthorizedAccessException("Token inválido.");

            var conta = await _contaRepo.ObterPorIdAsync(contaId);
            if (conta == null)
                throw new Exception("Conta não encontrada. TIPO: INVALID_ACCOUNT");

            if (!conta.Ativo)
                throw new Exception("Conta inativa. TIPO: INACTIVE_ACCOUNT");

            var saldo = await _movimentoRepo.CalcularSaldoAsync(contaId);

            return new SaldoResult
            {
                Numero = conta.Numero,
                Nome = conta.Nome,
                DataHora = DateTime.UtcNow,
                Saldo = saldo
            };
        }
    }
}
