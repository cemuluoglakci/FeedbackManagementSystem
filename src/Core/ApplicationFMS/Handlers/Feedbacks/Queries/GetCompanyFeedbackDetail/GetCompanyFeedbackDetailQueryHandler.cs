using ApplicationFMS.Interfaces;
using ApplicationFMS.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationFMS.Handlers.Feedbacks.Queries.GetCompanyFeedbackDetail
{
    public class GetCompanyFeedbackDetailQueryHandler : IRequestHandler<GetCompanyFeedbackDetailQuery, BaseResponse>
    {
        private readonly IFMSDataContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUser _currentUser;

        public GetCompanyFeedbackDetailQueryHandler(IFMSDataContext context, IMapper mapper, ICurrentUser? currentUser)
        {
            _context = context;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<BaseResponse> Handle(GetCompanyFeedbackDetailQuery request, CancellationToken cancellationToken)
        {
            var vm = await _context.Feedback
                .Where(e => e.Id == request.Id && e.IsActive)
                .ProjectTo<GetCompanyFeedbackDetailVm>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(cancellationToken);

            if (vm == null)
            {
                return BaseResponse.Fail("No active feedback found.");
            }
            if (_currentUser.NotInCompany(vm.CompanyId))
            {
                return BaseResponse.Fail("Feedback is not related with your company.");
            }

            return new BaseResponse(vm);

        }

    }


}
