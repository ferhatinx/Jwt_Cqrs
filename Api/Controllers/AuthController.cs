using Api.Core.Application.Features.CQRS.CheckUserMediatR;
using Api.Core.Application.Features.CQRS.RegisterUserMediatR;
using Api.Infrastructere;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserCommandRequest rq)
        {
             await _mediator.Send(rq);
             return Ok();
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Login(CheckUserQueryRequest rq)
        {
            var response = await _mediator.Send(rq);
            if (response.isExist)
            {
                return Created("",new JwtTokenGenerator().GenerateToken(response));
            }
            return BadRequest("Kullanıcı adı veya şifre hatalı ");
        }
    }
}
