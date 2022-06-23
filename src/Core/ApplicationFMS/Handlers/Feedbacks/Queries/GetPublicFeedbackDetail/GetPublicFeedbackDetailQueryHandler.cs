using System.Collections.Generic;
using ApplicationFMS.Interfaces;
using ApplicationFMS.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreFMS.Entities;
using NotImplementedException = System.NotImplementedException;
using ApplicationFMS.Handlers.Comments.Queries.GetCommentDetail;

namespace ApplicationFMS.Handlers.Feedbacks.Queries.GetPublicFeedbackDetail
{
    public class GetPublicFeedbackDetailQueryHandler : IRequestHandler<GetPublicFeedbackDetailQuery, BaseResponse>
    {
        private readonly IFMSDataContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUser? _currentUser;
        private GetPublicFeedbackDetailVm? _viewModel;

        public GetPublicFeedbackDetailQueryHandler(IFMSDataContext context, IMapper mapper, ICurrentUser? currentUser)
        {
            _context = context;
            _mapper = mapper;
            _currentUser = currentUser;
            _viewModel = null;
        }

        public async Task<BaseResponse> Handle(GetPublicFeedbackDetailQuery request, CancellationToken cancellationToken)
        {
            var vmQuery = _context.Feedback.Where(e => e.Id == request.Id && e.IsActive);

            _viewModel = await vmQuery.ProjectTo<GetPublicFeedbackDetailVm>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(cancellationToken);

            if (_viewModel == null) return BaseResponse.Fail("No active feedback found.");

            if (_currentUser?.UserDetail?.Id == null) return new BaseResponse(_viewModel);

            SetUserReaction();

            SetIsMineAttribute(vmQuery.First().UserId);

            SetCommentList(request.Id);

            return new BaseResponse(_viewModel);

        }

        private void SetCommentList(int feedbackId)
        {
            var commentList = _context.Comment.Where(x => x.FeedbackId == feedbackId && x.ParentCommentId == null && x.IsActive)
                .ProjectTo<CommentDto>(_mapper.ConfigurationProvider).ToList();

            foreach (var commentDto in commentList)
            {
                commentDto.ChildComment = GetChildComments(commentDto.Id);
            }

            _viewModel.CommentList = commentList;
        }

        private ICollection<CommentDto> GetChildComments(int parentCommentId)
        {
            var commentList = _context.Comment.Where(x => x.ParentCommentId == parentCommentId && x.IsActive)
                .ProjectTo<CommentDto>(_mapper.ConfigurationProvider).ToList();
            foreach (var commentDto in commentList)
            {
                commentDto.ChildComment = GetChildComments(commentDto.Id);
            }

            return commentList;
        }

        private void SetIsMineAttribute(int userId)
        {
            if (_currentUser.UserDetail.Id == userId)
            {
                _viewModel.IsMine = true;
            }
        }

        private void SetUserReaction()
        {
            var userReaction = _context.ReactionFeedback
                .FirstOrDefault(x => x.IsActive &&
                                     x.UserId == _currentUser.UserDetail.Id &&
                                     x.FeedbackId == _viewModel.Id);
            _viewModel.UserReaction = userReaction?.Sentiment;
        }
    }
}
