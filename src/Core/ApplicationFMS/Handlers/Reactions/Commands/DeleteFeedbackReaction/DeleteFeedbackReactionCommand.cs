using ApplicationFMS.Models;
using MediatR;

namespace ApplicationFMS.Handlers.Reactions.Commands.DeleteFeedbackReaction
{
    public class DeleteFeedbackReactionCommand : IRequest<BaseResponse<int>>
    {
        public int FeedbackId { get; set; }
    }
}
