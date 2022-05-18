using System.Linq;
using ApplicationFMS.Handlers.System.Queries.GetOperationalMode;
using ApplicationFMS.Interfaces;
using ApplicationFMS.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ApplicationFMS.Helpers;

namespace ApplicationFMS.Handlers.System.Queries.GetTimeoutDuration
{
    public class GetTimeoutDurationQueryHandler : IRequestHandler<GetTimeoutDurationQuery, BaseResponse>
    {
        private readonly IFMSDataContext _context;
        public GetTimeoutDurationQueryHandler(IFMSDataContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse> Handle(GetTimeoutDurationQuery request, CancellationToken cancellationToken)
        {
            int timeoutDurtion = _context.System.FirstOrDefault(x => x.SystemVariable == Constants.SystemVariableTimeoutName).Value;

            return new BaseResponse(timeoutDurtion);
        }
    }
}
