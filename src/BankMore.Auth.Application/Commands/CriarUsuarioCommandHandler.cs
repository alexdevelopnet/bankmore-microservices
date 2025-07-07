using BankMore.Auth.Domain.Entities;
using BankMore.Auth.Domain.Repositories;
using BankMore.Auth.Domain.ValueObjects;
using MediatR;
namespace BankMore.Auth.Application.Commands
{
    public class CriarUsuarioCommandHandler : IRequestHandler<CriarUsuarioCommand, Guid>
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public CriarUsuarioCommandHandler(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Guid> Handle(CriarUsuarioCommand request, CancellationToken cancellationToken)
        {
            if (!CPF.ValidarFormato(request.Cpf))
                throw new ArgumentException("CPF inválido.");

            var existenteCpf = await _usuarioRepository.ObterPorCpfAsync(request.Cpf);
            if (existenteCpf != null)
                throw new ArgumentException("CPF já cadastrado.");

            var existenteEmail = await _usuarioRepository.ObterPorEmailAsync(request.Email);
            if (existenteEmail != null)
                throw new ArgumentException("Email já cadastrado.");

            var cpf = new CPF(request.Cpf);

            var email = new Email(request.Email);  

            var senhaHash = BCrypt.Net.BCrypt.HashPassword(request.Senha);

            var usuario = new Usuario(
                Guid.NewGuid(),
                request.Nome,
                cpf,
                email,  
                senhaHash
            );

            await _usuarioRepository.AdicionarAsync(usuario);

            return usuario.Id;
        }
    }
}
