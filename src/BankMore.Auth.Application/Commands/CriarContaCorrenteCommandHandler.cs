using BankMore.Auth.Domain.Entities;
using BankMore.Auth.Domain.Repositories;
using MediatR;
using System.Security.Cryptography;
using System.Text;

namespace BankMore.Auth.Application.Commands
{
    public class CriarContaCorrenteCommandHandler : IRequestHandler<CriarContaCorrenteCommand, Guid>
    {
        private readonly IContaCorrenteRepository _repository;

        public CriarContaCorrenteCommandHandler(IContaCorrenteRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(CriarContaCorrenteCommand request, CancellationToken cancellationToken)
        {
            if (await _repository.NumeroExisteAsync(request.Numero))
                throw new ArgumentException("Número da conta já existe.");

            var salt = Guid.NewGuid().ToString("N");
            var senhaHash = HashSenha(request.Senha, salt);

            var conta = new ContaCorrente(
                Guid.NewGuid(),
                request.Numero,
                request.Nome,
                senhaHash,
                salt
            );

            await _repository.AdicionarAsync(conta);
            return conta.Id;
        }

        private string HashSenha(string senha, string salt)
        {
            using var sha256 = SHA256.Create();
            var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(senha + salt));
            return Convert.ToBase64String(hash);
        }
    }
}
