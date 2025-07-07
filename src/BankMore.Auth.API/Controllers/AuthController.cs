using BankMore.Auth.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BankMore.Auth.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] CriarUsuarioCommand command)
        {
            var id = await _mediator.Send(command);
            return Created("", new { UsuarioId = id });
        }
    }
}
