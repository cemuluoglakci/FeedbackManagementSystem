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

namespace ApplicationFMS.Handlers.LookUp.LookupRole
{
    public class LookupRoleHandler : IRequestHandler<LookupRoleListQuery, RoleListVm>
    {
        private readonly IFMSDataContext _context;
        private readonly IMapper _mapper;

        public LookupRoleHandler(IFMSDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<RoleListVm> Handle(LookupRoleListQuery request, CancellationToken cancellationToken)
        {
            var lookupList = await _context.Role
                .ProjectTo<RoleDTO>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new RoleListVm
            {
                List = lookupList,
                Count = lookupList.Count
            };

            return vm;
        }
    }
}
