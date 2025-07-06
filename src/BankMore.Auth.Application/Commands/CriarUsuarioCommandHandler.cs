using System;
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
            var cpf = new CPF(request.Cpf);

            if (await _usuarioRepository.ObterPorCpfAsync(cpf.Numero) != null)
                throw new ApplicationException("Já existe um usuário com este CPF");

            var email = new Email(request.Email);
            var usuario = Usuario.Criar(request.Nome, cpf, email, request.Senha);
            await _usuarioRepository.AdicionarAsync(usuario);

            return usuario.Id;
        }
    }
}