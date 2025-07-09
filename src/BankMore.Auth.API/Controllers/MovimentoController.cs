using BankMore.Auth.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BankMore.Auth.API.Controllers
{
    [ApiController]
    [Route("api/movimentos")]
    public class MovimentoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MovimentoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Criar(CriarMovimentoCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(null, new { id });
        }
    }
}
 
