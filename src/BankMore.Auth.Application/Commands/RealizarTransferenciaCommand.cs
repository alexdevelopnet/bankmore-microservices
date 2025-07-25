﻿using MediatR;

namespace BankMore.Auth.Application.Commands
{
    public record RealizarTransferenciaCommand(Guid IdContaOrigem, Guid IdContaDestino, decimal Valor,string ChaveIdempotencia) : IRequest<Guid>;
}
