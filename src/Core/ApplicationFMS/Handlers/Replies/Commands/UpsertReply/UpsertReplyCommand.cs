using ApplicationFMS.Models;
using MediatR;

namespace ApplicationFMS.Handlers.Replies.Commands.ReplyFeedback
{
    public class UpsertReplyCommand : IRequest<BaseResponse<int>>
    {
        public int? Id { get; set; }
        public int FeedbackId { get; set; }
        public string Text { get; set; } = null!;
    }
}
