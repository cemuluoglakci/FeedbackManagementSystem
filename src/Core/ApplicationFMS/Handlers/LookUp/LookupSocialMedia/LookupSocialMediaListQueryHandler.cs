using ApplicationFMS.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationFMS.Handlers.LookUp.LookupSocialMedia
{
    public class LookupSocialMediaListQueryHandler : IRequestHandler<LookupSocialMediaListQuery, SocialMediaListVm>
    {
        private readonly IFMSDataContext _context;
        private readonly IMapper _mapper;

        public LookupSocialMediaListQueryHandler(IFMSDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SocialMediaListVm> Handle(LookupSocialMediaListQuery request, CancellationToken cancellationToken)
        {
            var lookupList = await _context.SocialMedia
                .ProjectTo<SocialMediaDTO>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new SocialMediaListVm
            {
                List = lookupList,
                Count = lookupList.Count
            };

            return vm;
        }

    }
}
