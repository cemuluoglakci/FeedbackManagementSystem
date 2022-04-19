using ApplicationFMS.Handlers.Comments.Commands.PostComment;
using ApplicationFMS.Helpers;
using ApplicationFMS.Models;
using FmsAPI.Helper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FmsAPI.Controllers
{
    public class CommentController : BaseController
    {
        [HttpPost]
        [Authorize(Constants.CustomerRole)]
        public async Task<ActionResult<BaseResponse<int>>> PostComment([FromBody] PostCommentCommand request)
        {
            var vm = await Mediator.Send(request);
            return base.Ok(vm);
        }
    }
}
