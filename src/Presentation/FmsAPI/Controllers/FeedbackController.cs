using ApplicationFMS.Handlers.Feedbacks.Commands.PostFeedback;
using ApplicationFMS.Handlers.Feedbacks.Queries.GetPublicFeedbackList;
using ApplicationFMS.Handlers.UserHandlers.Commands.ToggleUserAbility;
using ApplicationFMS.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using FmsAPI.Helper;
using ApplicationFMS.Handlers.Feedbacks.Commands.ToggleActive;

namespace FmsAPI.Controllers
{
    public class FeedbackController : BaseController
    {
        [HttpPost]
        public async Task<ActionResult<BaseResponse<FeedbackListVm>>> GetList([FromBody] GetFeedbackListQuery request)
        {
            var vm = await Mediator.Send(request);
            return base.Ok(vm);
        }

        [HttpPost]
        [Authorize("Customer")]
        public async Task<ActionResult<BaseResponse<int>>> PostFeedback([FromBody] PostFeedbackCommand request)
        {
            var vm = await Mediator.Send(request);
            return base.Ok(vm);
        }

        [HttpGet("{id}")]
        [Authorize("System Administrator")]
        public async Task<ActionResult<BaseResponse<int>>> ToggleFeedbackAbility(int id)
        {
            return base.Ok(await Mediator.Send(new ToggleActiveFeedbackCommand { Id = id }));
        }

    }
}
