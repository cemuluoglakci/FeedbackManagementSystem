using ApplicationFMS.Handlers.Feedbacks.Queries.GetPublicFeedbackDetail;
using Microsoft.AspNetCore.Mvc;

namespace FmsWebUI.ViewComponents
{
    public class SingleComment : ViewComponent
    {
        public IViewComponentResult Invoke(CommentDto commentDto)
        {
            return View(commentDto);
        }
    }
}
