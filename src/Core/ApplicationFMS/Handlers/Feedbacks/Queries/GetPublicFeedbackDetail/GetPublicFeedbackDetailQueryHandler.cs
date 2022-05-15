using ApplicationFMS.Interfaces;
using ApplicationFMS.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationFMS.Handlers.Feedbacks.Queries.GetPublicFeedbackDetail
{
    public class GetPublicFeedbackDetailQueryHandler : IRequestHandler<GetPublicFeedbackDetailQuery, BaseResponse<GetPublicFeedbackDetailVm>>
    {
        private readonly IFMSDataContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUser? _currentUser;

        public GetPublicFeedbackDetailQueryHandler(IFMSDataContext context, IMapper mapper, ICurrentUser? currentUser)
        {
            _context = context;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<BaseResponse<GetPublicFeedbackDetailVm>> Handle(GetPublicFeedbackDetailQuery request, CancellationToken cancellationToken)
        {
            var vmQuery = _context.Feedback.Where(e => e.Id == request.Id && e.IsActive);

            var vm = await vmQuery.ProjectTo<GetPublicFeedbackDetailVm>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(cancellationToken);

            if (vm == null)
            {
                return BaseResponse<GetPublicFeedbackDetailVm>.Fail("No active feedback found.");
            }

            if (_currentUser?.UserDetail?.Id != null)
            {
                var userReaction = _context.ReactionFeedback
                    .FirstOrDefault(x => x.IsActive && 
                    x.UserId == _currentUser.UserDetail.Id &&
                    x.FeedbackId == vm.Id);

                vm.UserReaction = userReaction?.Sentiment;

                if(_currentUser.UserDetail.Id == vmQuery.First().UserId)
                {
                    vm.IsMine = true;
                }
            }

            return new BaseResponse<GetPublicFeedbackDetailVm>(vm);

        }

    }
}
