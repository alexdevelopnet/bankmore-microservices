using BankMore.Auth.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BankMore.Auth.Application.Commands
{
    public class InativarContaCommandHandler : IRequestHandler<InativarContaCommand, Unit>
    {
        private readonly IContaCorrenteRepository _repository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public InativarContaCommandHandler(IContaCorrenteRepository repository, IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Unit> Handle(InativarContaCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst("id")?.Value;
 
            if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out var contaId))
                throw new UnauthorizedAccessException();

            var conta = await _repository.ObterPorIdAsync(contaId);
            if (conta is null)
                throw new ArgumentException("Conta não encontrada", "INVALID_ACCOUNT");

            if (!conta.Ativo)
                throw new ArgumentException("Conta já está inativa", "INACTIVE_ACCOUNT");

            var hashInformado = HashSenha(request.Senha, conta.Salt);
            if (hashInformado != conta.Senha)
                throw new ArgumentException("Senha incorreta", "INVALID_PASSWORD");

            conta.Inativar();
            await _repository.AtualizarAsync(conta);

            return Unit.Value;
        }

        private string HashSenha(string senha, string salt)
        {
            using var sha256 = SHA256.Create();
            var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(senha + salt));
            return Convert.ToBase64String(hash);
        }
    }
}
