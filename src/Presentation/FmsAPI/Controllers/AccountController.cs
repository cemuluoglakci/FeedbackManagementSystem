using ApplicationFMS.Handlers.Account.Commands.RegisterUser;
using ApplicationFMS.Handlers.Account.Commands.VerifyEmail;
using ApplicationFMS.Handlers.Account.Queries.UserLogin;
using ApplicationFMS.Models;
using CoreFMS.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FmsAPI.Controllers
{
    public class AccountController : BaseController
    {
        [HttpPost]
        public async Task<ActionResult<BaseResponse<User>>> RegisterUser([FromBody] RegisterUserCommand request)
        {
            var vm = await Mediator.Send(request);
            return base.Ok(vm);
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<string>>> UserLogin([FromBody] UserLoginQuery request)
        {
            var loginResponse = await Mediator.Send(request);
            return base.Ok(loginResponse);
        }

        [HttpGet]
        public async Task<ActionResult<string>> VerifyEmail(string email, string verificationCode)
        {
            var command = new VerifyEmailCommand() { Email = email, VerificationCode = verificationCode };
            return base.Ok(await Mediator.Send(command));
        }
    }
}
