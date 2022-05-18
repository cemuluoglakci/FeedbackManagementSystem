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

namespace ApplicationFMS.Handlers.LookUp.LookupEmployees
{
    public class LookupEmployeeListQueryHandler : IRequestHandler<LookupEmployeeListQuery, EmployeeListVm>
    {
        private readonly IFMSDataContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUser? _currentUser;

        public LookupEmployeeListQueryHandler(IFMSDataContext context, IMapper mapper, ICurrentUser? currentUser)
        {
            _context = context;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<EmployeeListVm> Handle(LookupEmployeeListQuery request, CancellationToken cancellationToken)
        {
            var lookupList = await _context.User.Where(x => x.IsActive &&
                    x.CompanyId == _currentUser.UserDetail.CompanyId )
                .ProjectTo<EmployeeDTO>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new EmployeeListVm
            {
                List = lookupList,
                Count = lookupList.Count
            };

            return vm;
        }
    }
}
