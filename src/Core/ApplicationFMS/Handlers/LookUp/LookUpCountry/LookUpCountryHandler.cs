using ApplicationFMS.Handlers.LookUp.LookUpCountry;
using ApplicationFMS.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationFMS.LookUp.Country
{
    public class LookUpCountryHandler : IRequestHandler<LookupCountryListQuery, CountryListVm>
    {
        private readonly IFMSDataContext _context;
        private readonly IMapper _mapper;

        public LookUpCountryHandler(IFMSDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CountryListVm> Handle(LookupCountryListQuery request, CancellationToken cancellationToken)
        {
            var countryList = await _context.Country
                .ProjectTo<CountryDTO>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new CountryListVm
            {
                CountryList = countryList,
                Count = countryList.Count
            };

            return vm;
        }
    }
}
