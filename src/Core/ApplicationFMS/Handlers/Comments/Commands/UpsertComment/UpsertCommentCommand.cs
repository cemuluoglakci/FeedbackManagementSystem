using ApplicationFMS.Models;
using MediatR;

namespace ApplicationFMS.Handlers.Comments.Commands.UpsertComment
{
    public class UpsertCommentCommand : IRequest<BaseResponse>
    {
        public int? Id { get; set; }
        public int FeedbackId { get; set; }
        public int? ParentCommentId { get; set; }
        public string Text { get; set; } = null!;
        public bool IsAnonym { get; set; }

    }
}
