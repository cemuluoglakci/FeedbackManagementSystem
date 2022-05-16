using ApplicationFMS.Helpers;
using ApplicationFMS.Interfaces;
using ApplicationFMS.Models;
using CoreFMS.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationFMS.Handlers.Comments.Commands.UpsertComment
{
    public class UpsertCommentCommandHandler : IRequestHandler<UpsertCommentCommand, BaseResponse>
    {
        private readonly IFMSDataContext _context;
        private readonly ICurrentUser? _currentUser;

        public UpsertCommentCommandHandler(IFMSDataContext context, ICurrentUser? currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<BaseResponse> Handle(UpsertCommentCommand request, CancellationToken cancellationToken)
        {
            if (_currentUser.NotInRole(Constants.CustomerRole))
            {
                return BaseResponse.Fail("If you want to contribute to the system with comments please create a 'Customer' account with a dedicated E-mail address.");
            }

            Feedback feedback = _context.Feedback.Find(request.FeedbackId);
            if (feedback == null)
            {
                return BaseResponse.Fail("Related feedback was not found.");
            }

            if (ParentCommentIdIsInvalid(request.ParentCommentId, feedback.Id))
            {
                BaseResponse.Fail("Invalid parent comment");
            }

            Comment entity;

            if (request.Id > 0)
            {
                entity = await _context.Comment.FindAsync(request.Id.Value);
                if (!_currentUser.HasSameId(entity.UserId))
                {
                    return BaseResponse.Fail("Users can only edit their own posts");
                }
            }
            else
            {
                entity = new Comment()
                {
                    FeedbackId = request.FeedbackId,
                    UserId = _currentUser.UserDetail.Id,
                    IsActive = true,
                    IsChecked = false,
                    CreatedAt = DateTime.Now,
                    ParentCommentId = request.ParentCommentId
                };
                _context.Comment.Add(entity);
            }

            entity.Text = request.Text;
            entity.IsAnonym = request.IsAnonym;

            await _context.SaveChangesAsync(cancellationToken);
            return new BaseResponse(entity.Id);
        }

        private bool ParentCommentIdIsInvalid(int? parentCommentId, int feedbackId)
        {
            if (parentCommentId > 0)
            {
                Comment parentComment = _context.Comment.Find(parentCommentId);
                if (parentComment == null)
                {
                    return false;
                }
                if (feedbackId != parentComment.FeedbackId)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
