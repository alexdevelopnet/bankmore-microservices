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
        public async Task<IActionResult> RealizarTransferencia(RealizarTransferenciaCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(null, new { id });
        }
    }
}

