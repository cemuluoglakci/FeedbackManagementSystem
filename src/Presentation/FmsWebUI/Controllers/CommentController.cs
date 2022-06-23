using ApplicationFMS.Handlers.Comments.Commands.DeleteComment;
using ApplicationFMS.Handlers.Comments.Commands.UpsertComment;
using ApplicationFMS.Handlers.Comments.Queries.GetCommentDetail;
using ApplicationFMS.Handlers.Comments.Queries.GetOwnCommentList;
using ApplicationFMS.Handlers.Feedbacks.Commands.UpsertFeedback;
using ApplicationFMS.Handlers.Feedbacks.Queries.GetPlainFeedback;
using ApplicationFMS.Handlers.Feedbacks.Queries.GetPublicFeedbackList;
using ApplicationFMS.Helpers;
using AutoMapper;
using FmsWebUI.Helper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FmsWebUI.Controllers
{
    public class CommentController : BaseController
    {
        private readonly IMapper _mapper;
        public CommentController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var response = await Mediate(new GetCommentDetailQuery { Id = id });
            return View((CommentDetailDTO)response.data);
        }

        [HttpGet]
        [Authorize(Constants.CustomerRole)]
        public async Task<IActionResult> Upsert(int? commentId, int feedbackId, int? parentCommentId)
        {
            var commentDetail = new CommentDetailDTO { FeedbackId = feedbackId, ParentCommentId = parentCommentId };
            if (commentId > 0)
            {
                var commandResponse = await Mediate(new GetCommentDetailQuery { Id = (int)commentId });
                commentDetail = (CommentDetailDTO)commandResponse.data;
            }
            else
            {
                var feedbackDTOresponse = await Mediate(new GetPlainFeedbackQuery { Id = feedbackId });
                var feedbackDTO = (PublicFeedbackDTO)feedbackDTOresponse.data;
                commentDetail.FeedbackDTO = feedbackDTO;
            }
            return View(commentDetail);
        }

        [HttpPost]
        [Authorize(Constants.CustomerRole)]
        public async Task<IActionResult> Upsert(CommentDetailDTO commentDetail)
        {
            var command = _mapper.Map<UpsertCommentCommand>(commentDetail);
            var response = await Mediate(command);
            if (response.Meta.SuccessStatus)
                return RedirectToAction("GetDetail", "Feedback", new { id = commentDetail.FeedbackId });
            return View(commentDetail);
        }

        [HttpGet]
        [Authorize(Constants.CustomerRole)]
        public async Task<IActionResult> OwnPosts()
        {
            var response = await Mediate(new GetOwnCommentListQuery());
            return View((GetOwnCommentListVm)response.data);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await Mediate(new DeleteCommentCommand { Id = id });
            return RedirectToAction("OwnPosts");
        }

    }
}
