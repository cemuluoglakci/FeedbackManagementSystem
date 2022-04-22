using ApplicationFMS.Interfaces;
using ApplicationFMS.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationFMS.Handlers.Feedbacks.Queries.GetAdminFeedbackDetail
{
    public class GetAdminFeedbackDetailQueryHandler : IRequestHandler<GetAdminFeedbackDetailQuery, BaseResponse<GetAdminFeedbackDetailVm>>
    {
        private readonly IFMSDataContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUser _currentUser;

        public GetAdminFeedbackDetailQueryHandler(IFMSDataContext context, IMapper mapper, ICurrentUser? currentUser)
        {
            _context = context;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<BaseResponse<GetAdminFeedbackDetailVm>> Handle(GetAdminFeedbackDetailQuery request, CancellationToken cancellationToken)
        {
            var vm = await _context.Feedback
                .Where(e => e.Id == request.Id)
                .ProjectTo<GetAdminFeedbackDetailVm>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(cancellationToken);

            if (vm == null)
            {
                return BaseResponse<GetAdminFeedbackDetailVm>.Fail("No active feedback found.");
            }

            return new BaseResponse<GetAdminFeedbackDetailVm>(vm);
        }
    }
}
