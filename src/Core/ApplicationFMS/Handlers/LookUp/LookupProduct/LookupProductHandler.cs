using ApplicationFMS.Handlers.LookUp.LookupCompany;
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

namespace ApplicationFMS.Handlers.LookUp.LookupProduct
{
    public class LookupProductHandler : IRequestHandler<LookupProductListQuery, ProductListVm>
    {
        private readonly IFMSDataContext _context;
        private readonly IMapper _mapper;

        public LookupProductHandler(IFMSDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProductListVm> Handle(LookupProductListQuery request, CancellationToken cancellationToken)
        {
            var lookupList = await _context.Product
                .Where(x => x.CompanyId == request.CompanyId || request.CompanyId == null)
                .ProjectTo<ProductDTO>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new ProductListVm
            {
                List = lookupList,
                Count = lookupList.Count
            };

            return vm;
        }
    }
}
