using BankMore.Auth.Application.Commands;
using BankMore.Auth.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BankMore.Auth.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContaCorrenteController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ContaCorrenteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] CriarContaCorrenteCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(Criar), new { id }, new { id });
        }

        [HttpGet("contas/{id}/saldo")]
        public async Task<IActionResult> ObterSaldo(Guid id)
        {
            var saldo = await _mediator.Send(new ObterSaldoQuery(id));
            return Ok(saldo);
        }


    }
}
