using ApplicationFMS.Handlers.Feedbacks.Queries.GetPublicFeedbackList;
using Microsoft.AspNetCore.Mvc;

namespace FmsWebUI.ViewComponents
{
    public class SingleFeedbackInner : ViewComponent
    {
        public  IViewComponentResult Invoke(PublicFeedbackDTO publicFeedbackDto)
        {
            return View(publicFeedbackDto);
        }
    }
}
