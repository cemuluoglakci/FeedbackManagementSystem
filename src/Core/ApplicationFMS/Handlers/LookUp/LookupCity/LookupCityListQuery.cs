using ApplicationFMS.Handlers.LookUp.LookUpCity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationFMS.Handlers.LookUp.LookUpCountry
{
    public class LookupCityListQuery: IRequest<CityListVm>
    {
        public int? CountryId { get; set; }
    }
}
