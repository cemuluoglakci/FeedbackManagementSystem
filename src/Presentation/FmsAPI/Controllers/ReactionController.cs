using ApplicationFMS.Handlers.Reactions.Commands.DeleteCommentReaction;
using ApplicationFMS.Handlers.Reactions.Commands.DeleteFeedbackReaction;
using ApplicationFMS.Handlers.Reactions.Commands.ReactComment;
using ApplicationFMS.Handlers.Reactions.Commands.ReactFeedback;
using ApplicationFMS.Helpers;
using ApplicationFMS.Models;
using FmsAPI.Helper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FmsAPI.Controllers
{
    [Authorize]
    public class ReactionController : BaseController
    {
        [HttpPost]
        [Authorize(Constants.CustomerRole)]
        public async Task<ActionResult<BaseResponse>> ReactFeedback([FromBody] ReactFeedbackCommand request)
        {
            var vm = await Mediator.Send(request);
            return base.Ok(vm);
        }

        [HttpPost]
        [Authorize(Constants.CustomerRole)]
        public async Task<ActionResult<BaseResponse>> ReactComment([FromBody] ReactCommentCommand request)
        {
            var vm = await Mediator.Send(request);
            return base.Ok(vm);
        }

        [HttpDelete]
        [Authorize(Constants.CustomerRole)]
        public async Task<ActionResult<BaseResponse>> DeleteFeedbackReaction([FromBody] DeleteFeedbackReactionCommand request)
        {
            var vm = await Mediator.Send(request);
            return base.Ok(vm);
        }

        [HttpDelete]
        [Authorize(Constants.CustomerRole)]
        public async Task<ActionResult<BaseResponse>> DeleteCommentReaction([FromBody] DeleteCommentReactionCommand request)
        {
            var vm = await Mediator.Send(request);
            return base.Ok(vm);
        }

    }
}
