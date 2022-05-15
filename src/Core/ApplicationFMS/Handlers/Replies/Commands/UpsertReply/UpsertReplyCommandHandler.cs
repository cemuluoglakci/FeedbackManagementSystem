using ApplicationFMS.Interfaces;
using ApplicationFMS.Models;
using CoreFMS.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationFMS.Handlers.Replies.Commands.ReplyFeedback
{
    public class UpsertReplyCommandHandler : IRequestHandler<UpsertReplyCommand, BaseResponse>
    {
        private readonly IFMSDataContext _context;
        private readonly ICurrentUser? _currentUser;

        public UpsertReplyCommandHandler(IFMSDataContext context, ICurrentUser? currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }
        public async Task<BaseResponse> Handle(UpsertReplyCommand request, CancellationToken cancellationToken)
        {
            Feedback feedback = _context.Feedback.Find(request.FeedbackId);
            int currentUserId = _currentUser.UserDetail.Id;

            if (_currentUser.IsEligibleToReplyFeedback(feedback))
            {
                Reply entity;

                if (request.Id > 0)
                {
                    entity = await _context.Reply.FindAsync(request.Id.Value);
                    if (!_currentUser.HasSameId(entity.UserId))
                    {
                        return BaseResponse.Fail("Users can only edit their own posts");
                    }
                }
                else
                {
                    entity = new Reply
                    {
                        FeedbackId = request.FeedbackId,
                        UserId = currentUserId,
                        IsActive = true,
                        IsChecked = false,
                        CreatedAt = DateTime.Now,
                    };
                    _context.Reply.Add(entity);
                    feedback.IsReplied = true;
                }
                entity.Text = request.Text;

                await _context.SaveChangesAsync(cancellationToken);
                return new BaseResponse(entity.Id);
            }
            else
            {
                return BaseResponse.Fail("Appointed company employees should reply feedbacks. Once feedback is replied, also original poster can post reply.");
            }
        }
    }
}
