using MediatR;

namespace BankMore.Auth.Application.Commands
{
    public record CriarMovimentoCommand(Guid IdContaCorrente, decimal Valor, char Tipo) : IRequest<Guid>;
}
