using ApplicationFMS.Handlers.LookUp.LookupMode;
using ApplicationFMS.Interfaces;
using ApplicationFMS.Models;
using AutoMapper;
using MediatR;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using ApplicationFMS.Helpers;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace ApplicationFMS.Handlers.System.Queries.GetOperationalMode
{
    public class GetOperationalModeQueryHandler : IRequestHandler<GetOperationalModeQuery, BaseResponse>
    {
        private readonly IFMSDataContext _context;
        private readonly IMapper _mapper;

        public GetOperationalModeQueryHandler(IFMSDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<BaseResponse> Handle(GetOperationalModeQuery request, CancellationToken cancellationToken)
        {

            var modeId = _context.System.FirstOrDefault(x => x.SystemVariable == Constants.SystemVariableModeName).Value;
            var mode = await _context.OperationMode
                .ProjectTo<ModeDTO>(_mapper.ConfigurationProvider)
                .SingleAsync((x => x.Id == modeId ), cancellationToken);

            return new BaseResponse(mode);
        }

    }
}
