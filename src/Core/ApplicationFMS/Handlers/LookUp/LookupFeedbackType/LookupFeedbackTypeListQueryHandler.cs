using ApplicationFMS.Handlers.LookUp.LookupSector;
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

namespace ApplicationFMS.Handlers.LookUp.LookupFeedbackType
{
    public class LookupFeedbackTypeListQueryHandler : IRequestHandler<LookupFeedbackTypeListQuery, FeedbackTypeListVm>
    {
        private readonly IFMSDataContext _context;
        private readonly IMapper _mapper;

        public LookupFeedbackTypeListQueryHandler(IFMSDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<FeedbackTypeListVm> Handle(LookupFeedbackTypeListQuery request, CancellationToken cancellationToken)
        {
            var lookupList = await _context.FeedbackType
                .ProjectTo<FeedbackTypeDTO>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new FeedbackTypeListVm
            {
                List = lookupList,
                Count = lookupList.Count
            };

            return vm;
        }
    }
}
