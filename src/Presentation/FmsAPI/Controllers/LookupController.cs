using ApplicationFMS.Handlers.LookUp.LookUpCity;
using ApplicationFMS.Handlers.LookUp.LookupCompany;
using ApplicationFMS.Handlers.LookUp.LookupComplaintType;
using ApplicationFMS.Handlers.LookUp.LookUpCountry;
using ApplicationFMS.Handlers.LookUp.LookupEducation;
using ApplicationFMS.Handlers.LookUp.LookupEmployees;
using ApplicationFMS.Handlers.LookUp.LookupFeedbackType;
using ApplicationFMS.Handlers.LookUp.LookupMode;
using ApplicationFMS.Handlers.LookUp.LookupProduct;
using ApplicationFMS.Handlers.LookUp.LookupRole;
using ApplicationFMS.Handlers.LookUp.LookupSector;
using ApplicationFMS.Handlers.LookUp.LookupSocialMedia;
using ApplicationFMS.Helpers;
using FmsAPI.Helper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FmsAPI.Controllers
{
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

        [HttpGet]
        public async Task<ActionResult<SectorListVm>> Sector()
        {
            var vm = await Mediator.Send(new LookupSectorListQuery());
            return base.Ok(vm);
        }

        [HttpGet]
        public async Task<ActionResult<CompanyLisyVm>> Company(int? sectorId)
        {
            var vm = await Mediator.Send(new LookupCompanyListQuery { SectorId = sectorId });
            return base.Ok(vm);
        }

        [HttpGet]
        public async Task<ActionResult<ProductListVm>> Product(int? companyId)
        {
            var vm = await Mediator.Send(new LookupProductListQuery { CompanyId = companyId });
            return base.Ok(vm);
        }

        [HttpGet]
        public async Task<ActionResult<RoleListVm>> Role()
        {
            var vm = await Mediator.Send(new LookupRoleListQuery());
            return base.Ok(vm);
        }
        [HttpGet]
        public async Task<ActionResult<EducationListVm>> Education()
        {
            var vm = await Mediator.Send(new LookupEducationListQuery());
            return base.Ok(vm);
        }

        [HttpGet]
        public async Task<ActionResult<FeedbackTypeListVm>> FeedbackType()
        {
            var vm = await Mediator.Send(new LookupFeedbackTypeListQuery());
            return base.Ok(vm);
        }

        [HttpGet]
        public async Task<ActionResult<ComplaintTypeListVm>> ComplaintType()
        {
            var vm = await Mediator.Send(new LookupComplaintTypeListQuery());
            return base.Ok(vm);
        }

        [HttpGet]
        public async Task<ActionResult<SocialMediaListVm>> SocialMedia()
        {
            var vm = await Mediator.Send(new LookupSocialMediaListQuery());
            return base.Ok(vm);
        }

        [HttpGet]
        [Authorize(Constants.CompanyEmployeeRole, Constants.CompanyRepresentativeRole, Constants.CompanyManagerRole)]
        public async Task<ActionResult<EmployeeListVm>> Employee()
        {
            var vm = await Mediator.Send(new LookupEmployeeListQuery());
            return base.Ok(vm);
        }

        [HttpGet]
        [Authorize(Constants.AdminRole)]
        public async Task<ActionResult<ModeListVm>> Mode()
        {
            var vm = await Mediator.Send(new LookupModeListQuery());
            return base.Ok(vm);
        }
    }
}
