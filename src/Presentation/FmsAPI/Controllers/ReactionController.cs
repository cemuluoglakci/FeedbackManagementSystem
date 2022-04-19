using ApplicationFMS.Handlers.Reactions.Commands.ReactComment;
using ApplicationFMS.Handlers.Reactions.Commands.ReactFeedback;
using ApplicationFMS.Helpers;
using ApplicationFMS.Models;
using FmsAPI.Helper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FmsAPI.Controllers
{
    public class ReactionController : BaseController
    {
        [HttpPost]
        [Authorize(Constants.CustomerRole)]
        public async Task<ActionResult<BaseResponse<int>>> ReactFeedback([FromBody] ReactFeedbackCommand request)
        {
            var vm = await Mediator.Send(request);
            return base.Ok(vm);
        }

        [HttpPost]
        [Authorize(Constants.CustomerRole)]
        public async Task<ActionResult<BaseResponse<int>>> ReactComment([FromBody] ReactCommentCommand request)
        {
            var vm = await Mediator.Send(request);
            return base.Ok(vm);
        }
    }
}
