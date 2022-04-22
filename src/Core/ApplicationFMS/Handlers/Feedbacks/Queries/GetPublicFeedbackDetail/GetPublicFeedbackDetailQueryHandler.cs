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

        public GetPublicFeedbackDetailQueryHandler(IFMSDataContext context, IMapper mapper, ICurrentUser? currentUser)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<BaseResponse<GetPublicFeedbackDetailVm>> Handle(GetPublicFeedbackDetailQuery request, CancellationToken cancellationToken)
        {
            var vm = await _context.Feedback
                .Where(e => e.Id == request.Id && e.IsActive)
                .ProjectTo<GetPublicFeedbackDetailVm>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(cancellationToken);

            if (vm == null)
            {
                return BaseResponse<GetPublicFeedbackDetailVm>.Fail("No active feedback found.");
            }

            return new BaseResponse<GetPublicFeedbackDetailVm>(vm);

        }

    }
}
