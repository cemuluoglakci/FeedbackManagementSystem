using ApplicationFMS.Handlers.Replies.Commands.DeleteReply;
using ApplicationFMS.Handlers.Replies.Commands.UpsertReply;
using ApplicationFMS.Handlers.Replies.Commands.ToggleActive;
using ApplicationFMS.Handlers.Replies.Commands.ToggleChecked;
using ApplicationFMS.Helpers;
using ApplicationFMS.Models;
using FmsAPI.Helper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FmsAPI.Controllers
{
    [Authorize]
    public class ReplyController : BaseController
    {
        [HttpPost]
        [Authorize(Constants.CustomerRole, Constants.CompanyEmployeeRole)]
        public async Task<ActionResult<BaseResponse>> UpsertReply([FromBody] UpsertReplyCommand request)
        {
            return base.Ok(await Mediator.Send(request));
        }

        [HttpGet("{id}")]
        [Authorize(Constants.AdminRole)]
        public async Task<ActionResult<BaseResponse>> ToggleReplyChecked(int id)
        {
            return base.Ok(await Mediator.Send(new ToggleCheckedReplyCommand { Id = id }));
        }

        [HttpGet("{id}")]
        [Authorize(Constants.AdminRole)]
        public async Task<ActionResult<BaseResponse>> ToggleReplyAbility(int id)
        {
            return base.Ok(await Mediator.Send(new ToggleActiveReplyCommand { Id = id }));
        }

        [HttpDelete("{id}")]
        [Authorize(Constants.CustomerRole, Constants.CompanyEmployeeRole)]
        public async Task<IActionResult> Delete(int id)
        {
            return base.Ok(await Mediator.Send(new DeleteReplyCommand { Id = id }));
        }
    }
}
