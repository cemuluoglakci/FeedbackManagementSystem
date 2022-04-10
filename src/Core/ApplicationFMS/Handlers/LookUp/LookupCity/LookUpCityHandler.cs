using ApplicationFMS.Handlers.LookUp.LookUpCity;
using ApplicationFMS.Handlers.LookUp.LookUpCountry;
using ApplicationFMS.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationFMS.LookUp.Country
{
    public class LookUpCityHandler : IRequestHandler<LookupCityListQuery, CityListVm>
    {
        private readonly IFMSDataContext _context;
        private readonly IMapper _mapper;

        public LookUpCityHandler(IFMSDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CityListVm> Handle(LookupCityListQuery request, CancellationToken cancellationToken)
        {
            var cityList = await _context.City
                .ProjectTo<CityDTO>(_mapper.ConfigurationProvider)
                .Where(x => x.CountryId == request.CountryId || request.CountryId == null)
                .ToListAsync(cancellationToken);

            var vm = new CityListVm
            {
                CityList = cityList,
                Count = cityList.Count
            };

            return vm;
        }
    }
}
