using ApplicationFMS.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationFMS.Handlers.LookUp.LookupCompany
{
    public class LookupCompanyHandler : IRequestHandler<LookupCompanyListQuery, CompanyLisyVm>
    {
        private readonly IFMSDataContext _context;
        private readonly IMapper _mapper;

        public LookupCompanyHandler(IFMSDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CompanyLisyVm> Handle(LookupCompanyListQuery request, CancellationToken cancellationToken)
        {
            var lookupList = await _context.Company
                .Where(x => x.SectorId == request.SectorId || request.SectorId == null)
                .ProjectTo<CompanyDTO>(_mapper.ConfigurationProvider)

                .ToListAsync(cancellationToken);

            var vm = new CompanyLisyVm
            {
                List = lookupList,
                Count = lookupList.Count
            };

            return vm;
        }
    }
}
