using ApplicationFMS.Handlers.Feedbacks.Commands.PostFeedback;
using ApplicationFMS.Helpers;
using ApplicationFMS.Interfaces;
using ApplicationFMS.Models;
using CoreFMS.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationFMS.Handlers.Replies.Commands.ReplyFeedback
{
    public class ReplyFeedbackCommandHandler : IRequestHandler<ReplyFeedbackCommand, BaseResponse<int>>
    {
        private readonly IFMSDataContext _context;
        private readonly ICurrentUser? _currentUser;

        public ReplyFeedbackCommandHandler(IFMSDataContext context, ICurrentUser? currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }
        public async Task<BaseResponse<int>> Handle(ReplyFeedbackCommand request, CancellationToken cancellationToken)
        {
            if (_currentUser == null)
            {
                return new BaseResponse<int>(0, "User Identity could not defined.");
            }

            Feedback feedback = _context.Feedback.Find(request.FeedbackId);
            int currentUserId = _currentUser.UserDetail.Id;

            if (currentUserId == feedback.DirectedToEmploteeId || (currentUserId == feedback.UserId && feedback.IsReplied))
            {
                var entity = new Reply
                {
                    FeedbackId = request.FeedbackId,
                    UserId = currentUserId,
                    Text = request.Text,
                    IsActive = true,
                    IsChecked = false,
                    CreatedAt = DateTime.Now,
                };
                feedback.IsReplied = true;

                _context.Reply.Add(entity);
                await _context.SaveChangesAsync(cancellationToken);
                return new BaseResponse<int>(entity.Id);
            }
            else
            {
                return new BaseResponse<int>(0, "Appointed company employees should reply feedbacks. Once feedback is replied, also original poster can post reply." );
            }

        }
    }
}
