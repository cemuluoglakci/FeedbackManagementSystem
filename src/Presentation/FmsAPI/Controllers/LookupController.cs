﻿using ApplicationFMS.Handlers.LookUp.LookUpCity;
using ApplicationFMS.Handlers.LookUp.LookupCompany;
using ApplicationFMS.Handlers.LookUp.LookUpCountry;
using ApplicationFMS.Handlers.LookUp.LookupEducation;
using ApplicationFMS.Handlers.LookUp.LookupProduct;
using ApplicationFMS.Handlers.LookUp.LookupRole;
using ApplicationFMS.Handlers.LookUp.LookupSector;
using FmsAPI.Helper;
//using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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

    }
}
