using ApplicationFMS.Handlers.Feedbacks.Commands;
using ApplicationFMS.Handlers.Feedbacks.Queries.GetPublicFeedbackList;
using ApplicationFMS.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FmsAPI.Controllers
{
    public class FeedbackController : BaseController
    {
        [HttpPost]
        public async Task<ActionResult<BaseResponse<FeedbackListVm>>> GetList([FromBody]GetFeedbackListQuery request)
        {
            var vm = await Mediator.Send(request);
            return base.Ok(vm);
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<int>>> PostFeedback([FromBody] PostFeedbackCommand request)
        {
            var vm = await Mediator.Send(request);
            return base.Ok(vm);
        }

    }
}
