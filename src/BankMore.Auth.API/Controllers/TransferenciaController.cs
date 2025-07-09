using BankMore.Auth.API.Requests;
using BankMore.Auth.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BankMore.Auth.API.Controllers
{

    [ApiController]
    [Route("api/transferencias")]
    public class TransferenciaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TransferenciaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> RealizarTransferencia([FromBody] RealizarTransferenciaRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.ChaveIdempotencia))
                return BadRequest(new { Message = "Chave de idempotência é obrigatória." });

            var command = new RealizarTransferenciaCommand(
                request.IdContaOrigem,
                request.IdContaDestino,
                request.Valor,
                request.ChaveIdempotencia
            );

            var id = await _mediator.Send(command);

            if (id == Guid.Empty)
                return Conflict(new { Message = "Transferência já realizada anteriormente." });

            return CreatedAtAction(null, new { id });
        }
    }
}

