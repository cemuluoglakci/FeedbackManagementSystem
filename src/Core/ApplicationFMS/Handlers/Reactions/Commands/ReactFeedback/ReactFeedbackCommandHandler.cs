using ApplicationFMS.Helpers;
using ApplicationFMS.Interfaces;
using ApplicationFMS.Models;
using CoreFMS.Entities;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationFMS.Handlers.Reactions.Commands.ReactFeedback
{
    public class ReactFeedbackCommandHandler : IRequestHandler<ReactFeedbackCommand, BaseResponse>
    {
        private readonly IFMSDataContext _context;
        private readonly ICurrentUser? _currentUser;

        public ReactFeedbackCommandHandler(IFMSDataContext context, ICurrentUser? currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<BaseResponse> Handle(ReactFeedbackCommand request, CancellationToken cancellationToken)
        {
            if (_currentUser == null)
            {
                return new BaseResponse(0, "User Identity could not defined.");
            }
            if (_currentUser.UserDetail.RoleName != Constants.CustomerRole)
            {
                return new BaseResponse(0, "If you want to contribute to the system with reactions please create a 'Customer' account with a dedicated E-mail address.");
            }

            Feedback? feedback = _context.Feedback.Find(request.FeedbackId);
            if (feedback == null)
            {
                return new BaseResponse(0, "Feedback was not found.");
            }

            ReactionFeedback? possibleReaction = _context.ReactionFeedback.FirstOrDefault(x
                => x.FeedbackId == feedback.Id && x.UserId == _currentUser.UserDetail.Id && x.IsActive);

            if (possibleReaction != null && possibleReaction.Sentiment == request.Sentiment)
            {
                return new BaseResponse(0, "Reaction already stored.");
            }
            else if (possibleReaction != null)
            {
                DeleteReaction(possibleReaction.Id);
            }

            var entity = new ReactionFeedback
            {
                UserId = _currentUser.UserDetail.Id,
                FeedbackId = request.FeedbackId,
                Sentiment = request.Sentiment,
                IsActive = true,
                CreatedAt = DateTime.Now,
            };

            if (entity.Sentiment)
            {
                feedback.LikeCount++;
            }
            else
            {
                feedback.DislikeCount++;
            }

            _context.ReactionFeedback.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return new BaseResponse(entity.Id);
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
