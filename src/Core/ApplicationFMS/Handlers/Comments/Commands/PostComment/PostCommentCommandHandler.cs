using ApplicationFMS.Interfaces;
using ApplicationFMS.Models;
using ApplicationFMS.Helpers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using CoreFMS.Entities;
using System;
using System.Linq;

namespace ApplicationFMS.Handlers.Comments.Commands.PostComment
{
    public class PostCommentCommandHandler : IRequestHandler<PostCommentCommand, BaseResponse<int>>
    {
        private readonly IFMSDataContext _context;
        private readonly ICurrentUser? _currentUser;

        public PostCommentCommandHandler(IFMSDataContext context, ICurrentUser? currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<BaseResponse<int>> Handle(PostCommentCommand request, CancellationToken cancellationToken)
        {
            if (_currentUser == null)
            {
                return new BaseResponse<int>(0, "User Identity could not defined.");
            }
            if (_currentUser.UserDetail.RoleName != Constants.CustomerRole)
            {
                return new BaseResponse<int>(0, "If you want to contribute to the system with comments please create a 'Customer' account with a dedicated E-mail address.");
            }

            Feedback feedback = _context.Feedback.Find(request.FeedbackId);
            if (feedback == null)
            {
                return new BaseResponse<int>(0, "Related feedback was not found.");
            }

            Comment parentComment = _context.Comment.Find(request.ParentCommentId);
            if (parentComment == null)
            {
                return new BaseResponse<int>(0, "Parent comment was not found.");
            }
            if (feedback.Id != parentComment.FeedbackId)
            {
                return new BaseResponse<int>(0, "Inconsistency between feedback and parent comment.");
            }

            Comment entity = new Comment() 
            {
                FeedbackId = request.FeedbackId,
                UserId = _currentUser.UserDetail.Id,
                Text = request.Text,
                IsAnonym = request.IsAnonym,
                IsActive = true,
                IsChecked = false,
                CreatedAt = DateTime.Now,
                ParentCommentId = request.ParentCommentId
            };

            _context.Comment.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return new BaseResponse<int>(entity.Id);
        }
    }
}
