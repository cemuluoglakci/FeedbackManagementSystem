using ApplicationFMS.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationFMS.Handlers.LookUp.LookupEducation
{
    public class LookupEducationHandler : IRequestHandler<LookupEducationListQuery, EducationListVm>
    {
        private readonly IFMSDataContext _context;
        private readonly IMapper _mapper;

        public LookupEducationHandler(IFMSDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<EducationListVm> Handle(LookupEducationListQuery request, CancellationToken cancellationToken)
        {
            var lookupList = await _context.Education
                .ProjectTo<EducationDTO>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new EducationListVm
            {
                List = lookupList,
                Count = lookupList.Count
            };

            return vm;
        }
    }
}
