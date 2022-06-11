using ApplicationFMS.Models;
using MediatR;

namespace ApplicationFMS.Handlers.Reactions.Commands.DeleteFeedbackReaction
{
    public class DeleteFeedbackReactionCommand : IRequest<BaseResponse>
    {
        public int FeedbackId { get; set; }
    }
}
