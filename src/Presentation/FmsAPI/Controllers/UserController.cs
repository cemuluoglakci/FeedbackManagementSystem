using ApplicationFMS.Handlers.UserHandlers.Commands.ToggleUserAbility;
using ApplicationFMS.Handlers.UserHandlers.Commands.UpdateUser;
using ApplicationFMS.Handlers.UserHandlers.Queries.GetUserList;
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
        [Authorize("System Administrator", "Company Representative")]
        public async Task<ActionResult<BaseResponse<UserListVm>>> UserList([FromBody] GetUserListQuery request)
        {
            return base.Ok(await Mediator.Send(request));
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<BaseResponse<int>>> UpdateUser([FromBody] UpdateUserCommand request)
        {
            return base.Ok(await Mediator.Send(request));
        }

        [HttpGet("{id}")]
        [Authorize("System Administrator", "Company Representative")]
        public async Task<ActionResult<BaseResponse<int>>> ToggleUserAbility(int id)
        {
            return base.Ok(await Mediator.Send(new ToggleUserAbilityCommand { Id = id }));
        }
    }
}
