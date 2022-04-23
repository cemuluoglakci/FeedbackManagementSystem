using ApplicationFMS.Handlers.LookUp.LookUpCity;
using MediatR;

namespace ApplicationFMS.Handlers.LookUp.LookUpCountry
{
    public class LookupCityListQuery : IRequest<CityListVm>
    {
        public int? CountryId { get; set; }
    }
}
