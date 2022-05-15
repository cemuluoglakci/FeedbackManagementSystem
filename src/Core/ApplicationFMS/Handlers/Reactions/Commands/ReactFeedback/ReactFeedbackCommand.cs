using ApplicationFMS.Models;
using MediatR;

namespace ApplicationFMS.Handlers.Reactions.Commands.ReactFeedback
{
    public class ReactFeedbackCommand : IRequest<BaseResponse>
    {
        public int FeedbackId { get; set; }
        public bool Sentiment { get; set; }
    }
}
