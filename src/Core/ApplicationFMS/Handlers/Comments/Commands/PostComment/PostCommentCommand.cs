using ApplicationFMS.Models;
using MediatR;

namespace ApplicationFMS.Handlers.Comments.Commands.PostComment
{
    public class PostCommentCommand : IRequest<BaseResponse<int>>
    {
        public int FeedbackId { get; set; }
        public int? ParentCommentId { get; set; }
        public string Text { get; set; } = null!;
        public bool IsAnonym { get; set; }

    }
}
