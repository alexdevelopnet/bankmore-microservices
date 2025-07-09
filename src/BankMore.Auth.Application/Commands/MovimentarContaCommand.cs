using MediatR;

namespace BankMore.Auth.Application.Commands
{
    public record MovimentarContaCommand(
         string Tipo, // "C" ou "D"
         decimal Valor,
         string ChaveIdempotencia,
         int? NumeroConta = null
     ) : IRequest<Unit>;
}
