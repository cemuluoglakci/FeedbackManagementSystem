using ApplicationFMS.Handlers.Feedbacks.Commands.ToggleArchived;
using ApplicationFMS.Handlers.Replies.Commands.ReplyFeedback;
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
        [Authorize("Company Employee", "Customer")]
        public async Task<ActionResult<BaseResponse<int>>> PostReply ([FromBody] ReplyFeedbackCommand request)
        {
            return base.Ok(await Mediator.Send(request));
        }

    }
}
