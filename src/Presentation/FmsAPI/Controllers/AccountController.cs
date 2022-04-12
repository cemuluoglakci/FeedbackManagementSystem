using ApplicationFMS.Handlers.Account.Commands.RegisterUser;
using ApplicationFMS.Handlers.Account.Queries.UserLogin;
using ApplicationFMS.Handlers.LookUp.LookUpCountry;
using CoreFMS.Entities;
using CoreFMS.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FmsAPI.Controllers
{
    public class AccountController : BaseController
    {
        [HttpPost]
        public async Task<ActionResult<User>> RegisterUser([FromBody] RegisterUserCommand request)
        {
            var vm = await Mediator.Send(request);
            return base.Ok(vm);
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponse<string>>> UserLogin(string email, string password)
        {
            var loginResponse = await Mediator.Send(new UserLoginQuery {Email = email, Password = password });
            return base.Ok(loginResponse);
        }

    }
}
