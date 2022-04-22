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
            //_context.Configuration.LazyLoadingEnabled = false;
            var test = _context.Feedback
                .Include(x => x.Reply.Where(r => r.Id == 4))
                .Where(e => e.Id == request.Id && e.IsActive).SingleOrDefault();

            var vm = await _context.Feedback
                .Where(e => e.Id == request.Id && e.IsActive)
                .ProjectTo<GetPublicFeedbackDetailVm>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(cancellationToken);

            //var replyList = _context

            if (vm == null)
            {
                return BaseResponse<GetPublicFeedbackDetailVm>.Fail("No active feedback found.");
            }

            return new BaseResponse<GetPublicFeedbackDetailVm>(vm);

        }

    }
}
