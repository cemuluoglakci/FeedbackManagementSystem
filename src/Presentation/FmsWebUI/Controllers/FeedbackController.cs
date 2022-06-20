using ApplicationFMS.Handlers.Feedbacks.Queries.GetPublicFeedbackDetail;
using ApplicationFMS.Handlers.Feedbacks.Queries.GetPublicFeedbackList;
using ApplicationFMS.Helpers;
using FmsWebUI.Helper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ApplicationFMS.Handlers.Feedbacks.Commands.DeleteFeedback;
using ApplicationFMS.Handlers.Feedbacks.Commands.ToggleSolved;
using ApplicationFMS.Handlers.Feedbacks.Commands.UpsertFeedback;
using AutoMapper;
using ApplicationFMS.Handlers.Reactions.Commands.ReactFeedback;
using ApplicationFMS.Handlers.Reactions.Commands.DeleteFeedbackReaction;
using CoreFMS.Entities;
using ApplicationFMS.Models;

namespace FmsWebUI.Controllers
{

    public class FeedbackController : BaseController
    {

        private readonly IMapper _mapper;
        public FeedbackController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetDetail(int id)
        {
            var response = await Mediate(new GetPublicFeedbackDetailQuery { Id = id });
            return View((GetPublicFeedbackDetailVm)response.data);
        }


        [HttpGet]
        public ActionResult List(int? userId)
        {
            return View(new GetFeedbackListQuery{UserId = userId});
        }

        //[HttpGet]
        //public ActionResult List() => View();

        [HttpPost]
        public async Task<ViewComponentResult> LoadFeedback([FromBody] GetFeedbackListQuery request)
        {
            var feedbackList = (FeedbackListVm)(await Mediate(request)).data;
            TempData["filteredCountString"] = feedbackList.FilteredCount;
            return ViewComponent("MultipleFeedback", feedbackList);
        }

        [HttpGet]
        [Authorize(Constants.CustomerRole)]
        public async Task<IActionResult> Upsert(int? id)
        {
            var feedbackDetail = new GetPublicFeedbackDetailVm();
            if (id > 0)
            {
                feedbackDetail = (GetPublicFeedbackDetailVm)(await Mediate(new GetPublicFeedbackDetailQuery { Id = (int)id })).data;
            }
            var command = _mapper.Map<UpsertFeedbackCommand>(feedbackDetail);
            return View(command);
        }

        [HttpPost]
        [Authorize(Constants.CustomerRole)]
        public async Task<IActionResult> Upsert(UpsertFeedbackCommand command)
        {
            var response = await Mediate(command);
            if (response.Meta.SuccessStatus)
                return RedirectToAction("List");
            return View(command);
        }

        [HttpGet]
        [Authorize(Constants.CustomerRole)]
        public async Task<IActionResult> ToggleSolved(int id)
        {
            var response = await Mediate(new ToggleSolvedFeedbackCommand { Id = id });
            if (response.Meta.SuccessStatus)
                return RedirectToAction("GetDetail", new { id = id });
            return RedirectToAction("List");
        }

        [HttpGet]
        [Authorize(Constants.CustomerRole)]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await Mediate(new DeleteFeedbackCommand { Id = id });
            if (response.Meta.SuccessStatus)
                return RedirectToAction("List");
            return View();
        }

        [HttpGet]
        [Authorize(Constants.CustomerRole)]
        public async Task<IActionResult> React(int feedbackId, bool sentiment)
        {
            var response = await Mediate(new ReactFeedbackCommand { FeedbackId = feedbackId, Sentiment = sentiment });
            return RedirectToAction("GetDetail", new { id = feedbackId });
        }

        [HttpGet]
        [Authorize(Constants.CustomerRole)]
        public async Task<IActionResult> DeleteReaction(int feedbackId, bool sentiment)
        {
            var response = await Mediate(new DeleteFeedbackReactionCommand { FeedbackId = feedbackId });
            return RedirectToAction("GetDetail", new { id = feedbackId });
        }

    }
}
