using ApplicationFMS.Interfaces;
using ApplicationFMS.Models;
using CoreFMS.Entities;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationFMS.Handlers.Reactions.Commands.DeleteCommentReaction
{
    public class DeleteCommentReactionCommandHandler : IRequestHandler<DeleteCommentReactionCommand, BaseResponse<int>>
    {
        private readonly IFMSDataContext _context;
        private readonly ICurrentUser? _currentUser;

        public DeleteCommentReactionCommandHandler(IFMSDataContext context, ICurrentUser? currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<BaseResponse<int>> Handle(DeleteCommentReactionCommand request, CancellationToken cancellationToken)
        {
            if (_currentUser == null)
            {
                return new BaseResponse<int>(0, "Current User Identity was not defined.");
            }

            ReactionComment? reactionComment = _context.ReactionComment
                .FirstOrDefault(x =>
                x.CommentId == request.CommentId &&
                x.UserId == _currentUser.UserDetail.Id &&
                x.IsActive);

            if (reactionComment == null)
            {
                return new BaseResponse<int>(0, "No active Comment reaction found.");
            }
            if (reactionComment.UserId != _currentUser.UserDetail.Id)
            {
                return new BaseResponse<int>(0, "Only owner of the reaction can delete it.");
            }

            DeleteReaction(reactionComment.Id);
            await _context.SaveChangesAsync(cancellationToken);

            return new BaseResponse<int>(reactionComment.Id);
        }

        public void DeleteReaction(int ReactionId)
        {
            ReactionComment reactionComment = _context.ReactionComment.Find(ReactionId);
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
