using ApplicationFMS.Interfaces;
using ApplicationFMS.Models;
using CoreFMS.Entities;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationFMS.Handlers.Reactions.Commands.DeleteFeedbackReaction
{
    public class DeleteFeedbackReactionCommandHandler : IRequestHandler<DeleteFeedbackReactionCommand, BaseResponse>
    {
        private readonly IFMSDataContext _context;
        private readonly ICurrentUser? _currentUser;

        public DeleteFeedbackReactionCommandHandler(IFMSDataContext context, ICurrentUser? currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<BaseResponse> Handle(DeleteFeedbackReactionCommand request, CancellationToken cancellationToken)
        {
            if (_currentUser == null)
            {
                return new BaseResponse(0, "Current User Identity was not defined.");
            }

            ReactionFeedback? reactionFeedback = _context.ReactionFeedback
                .FirstOrDefault(x =>
                x.FeedbackId == request.FeedbackId &&
                x.UserId == _currentUser.UserDetail.Id &&
                x.IsActive);

            if (reactionFeedback == null)
            {
                return new BaseResponse(0, "No active feedback reaction found.");
            }
            if (reactionFeedback.UserId != _currentUser.UserDetail.Id)
            {
                return new BaseResponse(0, "Only owner of the reaction can delete it.");
            }

            DeleteReaction(reactionFeedback.Id);
            await _context.SaveChangesAsync(cancellationToken);

            return new BaseResponse(reactionFeedback.Id);
        }

        public void DeleteReaction(int id)
        {
            ReactionFeedback reactionFeedback = _context.ReactionFeedback.Find(id);
            reactionFeedback.IsActive = false;

            Feedback feedback = _context.Feedback.Find(reactionFeedback.FeedbackId);
            if (reactionFeedback.Sentiment)
            {
                feedback.LikeCount--;
            }
            else
            {
                feedback.DislikeCount--;
            }
        }

    }
}
