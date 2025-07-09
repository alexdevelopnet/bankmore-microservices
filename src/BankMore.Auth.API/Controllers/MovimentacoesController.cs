using BankMore.Auth.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankMore.Auth.API.Controllers
{
    [ApiController]
    [Route("api/movimentacoes")]
    [Authorize]
    public class MovimentacoesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MovimentacoesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Movimentar(MovimentarContaCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
