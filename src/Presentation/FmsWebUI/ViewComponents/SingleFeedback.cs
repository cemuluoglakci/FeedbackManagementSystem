using ApplicationFMS.Handlers.Feedbacks.Queries.GetPublicFeedbackList;
using Microsoft.AspNetCore.Mvc;

namespace FmsWebUI.ViewComponents
{
    public class SingleFeedback : ViewComponent
    {
        public  IViewComponentResult Invoke(PublicFeedbackDTO publicFeedbackDto)
        {
            return View(publicFeedbackDto);
        }
    }
}
