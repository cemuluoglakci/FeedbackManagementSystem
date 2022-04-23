using ApplicationFMS.Handlers.LookUp.LookupFeedbackType;
using ApplicationFMS.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationFMS.Handlers.LookUp.LookupMode
{
    public class LookupModeListQueryHandler : IRequestHandler<LookupModeListQuery, ModeListVm>
    {
        private readonly IFMSDataContext _context;
        private readonly IMapper _mapper;

        public LookupModeListQueryHandler(IFMSDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ModeListVm> Handle(LookupModeListQuery request, CancellationToken cancellationToken)
        {
            var lookupList = await _context.OperationMode
                .ProjectTo<ModeDTO>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new ModeListVm
            {
                List = lookupList,
                Count = lookupList.Count
            };

            return vm;
        }
    }
}
