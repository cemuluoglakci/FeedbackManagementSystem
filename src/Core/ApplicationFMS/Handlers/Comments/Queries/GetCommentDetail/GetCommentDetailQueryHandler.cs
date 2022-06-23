using ApplicationFMS.Handlers.Feedbacks.Queries.GetPublicFeedbackDetail;
using ApplicationFMS.Handlers.Feedbacks.Queries.GetPublicFeedbackList;
using ApplicationFMS.Interfaces;
using ApplicationFMS.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationFMS.Handlers.Comments.Queries.GetCommentDetail
{
    public class GetCommentDetailQueryHandler : IRequestHandler<GetCommentDetailQuery, BaseResponse>
    {
        private readonly IFMSDataContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUser? _currentUser;

        public GetCommentDetailQueryHandler(IFMSDataContext context, IMapper mapper, ICurrentUser? currentUser)
        {
            _context = context;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<BaseResponse> Handle(GetCommentDetailQuery request, CancellationToken cancellationToken)
        {
            var vmQuery = _context.Comment.Where(e => e.Id == request.Id && e.IsActive);

            var vm = await vmQuery.ProjectTo<CommentDetailDTO>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(cancellationToken);

            if (vm == null)
            {
                return BaseResponse.Fail("No active comment found.");
            }

            
            var relatedFeedbackQuery = _context.Feedback.Where(e => e.Id == vm.FeedbackId && e.IsActive);

            var relatedFeedback = await relatedFeedbackQuery.ProjectTo<PublicFeedbackDTO>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(cancellationToken);

            vm.FeedbackDTO = relatedFeedback;

            if (_currentUser?.UserDetail?.Id != null)
            {
                if (_currentUser.UserDetail.Id == vmQuery.First().UserId)
                {
                    vm.IsMine = true;
                }
            }

            return new BaseResponse(vm);

        }

    }
}
