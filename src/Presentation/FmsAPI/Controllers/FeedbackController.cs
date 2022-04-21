using ApplicationFMS.Handlers.Feedbacks.Commands.DeleteFeedback;
using ApplicationFMS.Handlers.Feedbacks.Commands.DirectFeedback;
using ApplicationFMS.Handlers.Feedbacks.Commands.ToggleActive;
using ApplicationFMS.Handlers.Feedbacks.Commands.ToggleArchived;
using ApplicationFMS.Handlers.Feedbacks.Commands.ToggleChecked;
using ApplicationFMS.Handlers.Feedbacks.Commands.ToggleSolved;
using ApplicationFMS.Handlers.Feedbacks.Commands.UpsertFeedback;
using ApplicationFMS.Handlers.Feedbacks.Queries.GetPublicFeedbackDetail;
using ApplicationFMS.Handlers.Feedbacks.Queries.GetPublicFeedbackList;
using ApplicationFMS.Helpers;
using ApplicationFMS.Models;
using FmsAPI.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FmsAPI.Controllers
{
    public class FeedbackController : BaseController
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse<GetPublicFeedbackDetailVm>>> Get(int id)
        {
            return Ok(await Mediator.Send(new GetPublicFeedbackDetailQuery { Id = id }));
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<FeedbackListVm>>> GetList([FromBody] GetFeedbackListQuery request)
        {
            var vm = await Mediator.Send(request);
            return base.Ok(vm);
        }

        [HttpPost]
        [Authorize(Constants.CustomerRole)]
        public async Task<ActionResult<BaseResponse<int>>> UpsertFeedback([FromBody] UpsertFeedbackCommand request)
        {
            var vm = await Mediator.Send(request);
            return base.Ok(vm);
        }

        [HttpPut]
        [Authorize(Constants.CompanyRepresentativeRole)]
        public async Task<ActionResult<BaseResponse<int>>> DirectFeedback([FromBody] DirectFeedbackCommand request)
        {
            var vm = await Mediator.Send(request);
            return base.Ok(vm);
        }

        [HttpGet("{id}")]
        [Authorize(Constants.AdminRole)]
        public async Task<ActionResult<BaseResponse<int>>> ToggleFeedbackAbility(int id)
        {
            return base.Ok(await Mediator.Send(new ToggleActiveFeedbackCommand { Id = id }));
        }

        [HttpGet("{id}")]
        [Authorize(Constants.AdminRole)]
        public async Task<ActionResult<BaseResponse<int>>> ToggleFeedbackChecked(int id)
        {
            return base.Ok(await Mediator.Send(new ToggleCheckedFeedbackCommand { Id = id }));
        }

        [HttpGet("{id}")]
        [Authorize(Constants.CustomerRole)]
        public async Task<ActionResult<BaseResponse<int>>> ToggleFeedbackSolved(int id)
        {
            return base.Ok(await Mediator.Send(new ToggleSolvedFeedbackCommand { Id = id }));
        }

        [HttpGet("{id}")]
        [Authorize(Constants.CompanyEmployeeRole)]
        public async Task<ActionResult<BaseResponse<int>>> ToggleFeedbackArchived(int id)
        {
            return base.Ok(await Mediator.Send(new ToggleArchivedFeedbackCommand { Id = id }));
        }

        [HttpDelete("{id}")]
        [Authorize(Constants.CustomerRole)]
        public async Task<IActionResult> Delete(int id)
        {
            return base.Ok(await Mediator.Send(new DeleteFeedbackCommand { Id = id }));
        }
    }
}
