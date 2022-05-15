using ApplicationFMS.Helpers;
using ApplicationFMS.Interfaces;
using ApplicationFMS.Models;
using CoreFMS.Entities;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationFMS.Handlers.Reactions.Commands.ReactComment
{
    public class ReactCommentCommandHandler : IRequestHandler<ReactCommentCommand, BaseResponse>
    {
        private readonly IFMSDataContext _context;
        private readonly ICurrentUser? _currentUser;

        public ReactCommentCommandHandler(IFMSDataContext context, ICurrentUser? currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }
        public async Task<BaseResponse> Handle(ReactCommentCommand request, CancellationToken cancellationToken)
        {
            if (_currentUser == null)
            {
                return new BaseResponse(0, "User Identity could not defined.");
            }
            if (_currentUser.UserDetail.RoleName != Constants.CustomerRole)
            {
                return new BaseResponse(0, "If you want to contribute to the system with reactions please create a 'Customer' account with a dedicated E-mail address.");
            }

            Comment? comment = _context.Comment.Find(request.CommentId);
            if (comment == null)
            {
                return new BaseResponse(0, "Comment was not found.");
            }

            ReactionComment? possibleReaction = _context.ReactionComment.FirstOrDefault(x
                => x.CommentId == comment.Id && x.UserId == _currentUser.UserDetail.Id && x.IsActive);

            if (possibleReaction != null && possibleReaction.Sentiment == request.Sentiment)
            {
                return new BaseResponse(0, "Reaction already stored.");
            }
            else if (possibleReaction != null)
            {
                DeleteReaction(possibleReaction.Id);
            }


            var entity = new ReactionComment
            {
                UserId = _currentUser.UserDetail.Id,
                CommentId = request.CommentId,
                Sentiment = request.Sentiment,
                IsActive = true,
                CreatedAt = DateTime.Now,
            };

            if (entity.Sentiment)
            {
                comment.LikeCount++;
            }
            else
            {
                comment.DislikeCount++;
            }

            _context.ReactionComment.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return new BaseResponse(entity.Id);
        }

        public void DeleteReaction(int id)
        {
            ReactionComment reactionComment = _context.ReactionComment.Find(id);
            reactionComment.IsActive = false;

            Comment? comment = _context.Comment.Find(reactionComment.CommentId);
            if (reactionComment.Sentiment)
            {
                comment.LikeCount--;
            }
            else
            {
                comment.DislikeCount--;
            }
        }
    }
}
