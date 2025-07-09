using BankMore.Auth.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BankMore.Auth.API.Controllers
{
    [ApiController]
    [Route("api/login")]
    public class LoginController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LoginController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Login(AutenticarUsuarioCommand command)
        {
            try
            {
                var token = await _mediator.Send(command);
                return Ok(new { token });
            }
            catch (UnauthorizedAccessException e)
            {
                return Unauthorized(new
                {
                    message = e.Message,
                    type = "USER_UNAUTHORIZED"
                });
            }
        }
    }
}
