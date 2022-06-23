using ApplicationFMS.Handlers.Comments.Queries.GetCommentDetail;
using Microsoft.AspNetCore.Mvc;

namespace FmsWebUI.ViewComponents
{
    public class CommentDetail : ViewComponent
    {
        public IViewComponentResult Invoke(CommentDetailDTO commentDetailDTO)
        {
            return View(commentDetailDTO);
        }
    }
}
