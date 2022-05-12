using ApplicationFMS.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationFMS.Handlers.LookUp.LookupComplaintType
{
    public class LookupComplaintTypeListQueryHandler : IRequestHandler<LookupComplaintTypeListQuery, ComplaintTypeListVm>
    {
        private readonly IFMSDataContext _context;
        private readonly IMapper _mapper;

        public LookupComplaintTypeListQueryHandler(IFMSDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ComplaintTypeListVm> Handle(LookupComplaintTypeListQuery request, CancellationToken cancellationToken)
        {
            var lookupList = await _context.FeedbackSubType
                .Where(x => x.IsActive)
                .ProjectTo<ComplaintTypeDTO>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new ComplaintTypeListVm
            {
                List = lookupList,
                Count = lookupList.Count
            };

            return vm;
        }
    }
}
