using ApplicationFMS.Handlers.Comments.Commands.PostComment;
using ApplicationFMS.Handlers.Comments.Commands.ToggleActive;
using ApplicationFMS.Handlers.Comments.Commands.ToggleChecked;
using ApplicationFMS.Handlers.Replies.Commands.ToggleActive;
using ApplicationFMS.Handlers.Replies.Commands.ToggleChecked;
using ApplicationFMS.Helpers;
using ApplicationFMS.Models;
using FmsAPI.Helper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FmsAPI.Controllers
{
    [Authorize]
    public class CommentController : BaseController
    {
        [HttpPost]
        [Authorize(Constants.CustomerRole)]
        public async Task<ActionResult<BaseResponse<int>>> UpsertComment([FromBody] UpsertCommentCommand request)
        {
            var vm = await Mediator.Send(request);
            return base.Ok(vm);
        }

        [HttpGet("{id}")]
        [Authorize(Constants.AdminRole)]
        public async Task<ActionResult<BaseResponse<int>>> ToggleCommentChecked(int id)
        {
            return base.Ok(await Mediator.Send(new ToggleCheckedCommentCommand { Id = id }));
        }

        [HttpGet("{id}")]
        [Authorize(Constants.AdminRole)]
        public async Task<ActionResult<BaseResponse<int>>> ToggleCommentAbility(int id)
        {
            return base.Ok(await Mediator.Send(new ToggleActiveCommentCommand { Id = id }));
        }
    }
}
