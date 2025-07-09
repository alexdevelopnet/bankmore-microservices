using MediatR;

namespace BankMore.Auth.Application.Commands
{
    public record AutenticarUsuarioCommand(string DocumentoOuConta, string Senha) : IRequest<string>;
}
