using MediatR;

namespace BankMore.Auth.Application.Commands
{
    public record CriarContaCorrenteCommand(string Nome, int Numero, string Senha) : IRequest<Guid>;
}
