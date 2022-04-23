using ApplicationFMS.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationFMS.Handlers.LookUp.LookupSector
{
    public class LookUpSectorHandler : IRequestHandler<LookupSectorListQuery, SectorListVm>
    {
        private readonly IFMSDataContext _context;
        private readonly IMapper _mapper;

        public LookUpSectorHandler(IFMSDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SectorListVm> Handle(LookupSectorListQuery request, CancellationToken cancellationToken)
        {
            var lookupList = await _context.Sector
                .ProjectTo<SectorDTO>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new SectorListVm
            {
                List = lookupList,
                Count = lookupList.Count
            };

            return vm;
        }
    }
}
