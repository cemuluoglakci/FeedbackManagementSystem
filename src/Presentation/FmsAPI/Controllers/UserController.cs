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
    }
}
