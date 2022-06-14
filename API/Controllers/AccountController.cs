using Application.DTO;
using Application.MediatR.AccountM.Comman;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [AllowAnonymous]
    public class AccountController : BaseApiController
    {
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto login)
        {
            return Ok(await Mediator.Send(new Login.Command { LoginDto = login }));
        }
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto register)
        {
            return Ok(await Mediator.Send(new Register.Command { RegisterDto = register }));
        }
    }
}
