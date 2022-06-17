using ApplicationFMS.Handlers.Feedbacks.Queries.GetPublicFeedbackDetail;
using ApplicationFMS.Handlers.Feedbacks.Queries.GetPublicFeedbackList;
using ApplicationFMS.Helpers;
using FmsWebUI.Helper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ApplicationFMS.Handlers.Feedbacks.Commands.UpsertFeedback;

namespace FmsWebUI.Controllers
{

    public class FeedbackController : BaseController
    {

        [HttpGet]
        public async Task<IActionResult> GetDetail(int id)
        {
            var response = await Mediate(new GetPublicFeedbackDetailQuery { Id = id });
            return View((GetPublicFeedbackDetailVm)response.data);
        }


        [HttpGet]
        public ActionResult List() => View();

        [HttpPost]
        public async Task<ViewComponentResult> LoadFeedback([FromBody] GetFeedbackListQuery request)
        {
            var feedbackList = (FeedbackListVm)(await Mediate(request)).data;
            return ViewComponent("MultipleFeedback", feedbackList);
        }

        [HttpGet]
        [Authorize(Constants.CustomerRole)]
        public async Task<IActionResult> Upsert()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Constants.CustomerRole)]
        public async Task<IActionResult> Upsert(UpsertFeedbackCommand command)
        {
            var response = await Mediate(command);
            return View();
        }

    }
}
