using ApplicationFMS.Handlers.Feedbacks.Queries.GetPublicFeedbackList;
using Microsoft.AspNetCore.Mvc;

namespace FmsWebUI.ViewComponents
{
    public class MultipleFeedback : ViewComponent
    {
        public  IViewComponentResult Invoke(FeedbackListVm feedbackListVm)
        {

            return View(feedbackListVm);
        }
    }
}
