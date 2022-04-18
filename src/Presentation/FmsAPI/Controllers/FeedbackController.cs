﻿using ApplicationFMS.Handlers.Feedbacks.Commands.PostFeedback;
using ApplicationFMS.Handlers.Feedbacks.Commands.ToggleActive;
using ApplicationFMS.Handlers.Feedbacks.Commands.ToggleArchived;
using ApplicationFMS.Handlers.Feedbacks.Commands.ToggleChecked;
using ApplicationFMS.Handlers.Feedbacks.Commands.ToggleSolved;
using ApplicationFMS.Handlers.Feedbacks.Queries.GetPublicFeedbackList;
using ApplicationFMS.Models;
using FmsAPI.Helper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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

        [HttpGet("{id}")]
        [Authorize("System Administrator")]
        public async Task<ActionResult<BaseResponse<int>>> ToggleFeedbackChecked(int id)
        {
            return base.Ok(await Mediator.Send(new ToggleCheckedFeedbackCommand { Id = id }));
        }

        [HttpGet("{id}")]
        [Authorize("Customer")]
        public async Task<ActionResult<BaseResponse<int>>> ToggleFeedbackSolved(int id)
        {
            return base.Ok(await Mediator.Send(new ToggleSolvedFeedbackCommand { Id = id }));
        }

        [HttpGet("{id}")]
        [Authorize("Company Employee")]
        public async Task<ActionResult<BaseResponse<int>>> ToggleFeedbackArchived(int id)
        {
            return base.Ok(await Mediator.Send(new ToggleArchivedFeedbackCommand { Id = id }));
        }
    }
}
