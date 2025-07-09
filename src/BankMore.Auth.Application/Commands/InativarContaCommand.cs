using MediatR;

namespace BankMore.Auth.Application.Commands
{
    public record InativarContaCommand(string Senha) : IRequest<Unit>;
}
