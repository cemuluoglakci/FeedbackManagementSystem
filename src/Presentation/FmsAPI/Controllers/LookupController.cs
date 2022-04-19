using ApplicationFMS.Handlers.LookUp.LookUpCity;
using ApplicationFMS.Handlers.LookUp.LookUpCountry;
//using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using FmsAPI.Helper;
using CoreFMS.Entities;

namespace FmsAPI.Controllers
{
    [Authorize]
    public class LookupController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<CountryListVm>> Country()
        {
            var vm = await Mediator.Send(new LookupCountryListQuery());
            return base.Ok(vm);
        }

        
        [HttpGet]
        public async Task<ActionResult<CityListVm>> City(int? countryId)
        {
            var vm = await Mediator.Send(new LookupCityListQuery { CountryId = countryId });
            return base.Ok(vm);
        }
    }
}
