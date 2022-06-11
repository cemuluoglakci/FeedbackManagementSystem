using ApplicationFMS.Models;
using MediatR;

namespace ApplicationFMS.Handlers.Replies.Commands.UpsertReply
{
    public class UpsertReplyCommand : IRequest<BaseResponse>
    {
        public int? Id { get; set; }
        public int FeedbackId { get; set; }
        public string Text { get; set; } = null!;
    }
}
