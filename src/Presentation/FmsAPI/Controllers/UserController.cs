using ApplicationFMS.Handlers.UserHandlers.Commands.ToggleUserAbility;
using ApplicationFMS.Handlers.UserHandlers.Commands.UpdateUser;
using ApplicationFMS.Handlers.UserHandlers.Queries.GetUserList;
using ApplicationFMS.Helpers;
using ApplicationFMS.Models;
using FmsAPI.Helper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FmsAPI.Controllers
{
    [Authorize]
    public class UserController : BaseController
    {
        [HttpPost]
        [Authorize(Constants.AdminRole, Constants.CompanyRepresentativeRole)]
        public async Task<ActionResult<BaseResponse>> GetList([FromBody] GetUserListQuery request)
        {
            return base.Ok(await Mediator.Send(request));
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<BaseResponse>> UpdateUser([FromBody] UpdateUserCommand request)
        {
            return base.Ok(await Mediator.Send(request));
        }

        [HttpGet("{id}")]
        [Authorize(Constants.AdminRole, Constants.CompanyRepresentativeRole)]
        public async Task<ActionResult<BaseResponse>> ToggleUserAbility(int id)
        {
            return base.Ok(await Mediator.Send(new ToggleUserAbilityCommand { Id = id }));
        }
    }
}
