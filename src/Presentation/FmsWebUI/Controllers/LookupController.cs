using ApplicationFMS.Handlers.Feedbacks.Queries.GetPublicFeedbackDetail;
using ApplicationFMS.Handlers.LookUp.LookupCompany;
using ApplicationFMS.Handlers.LookUp.LookUpCountry;
using ApplicationFMS.Handlers.LookUp.LookupEducation;
using ApplicationFMS.Handlers.LookUp.LookupFeedbackType;
using ApplicationFMS.Handlers.LookUp.LookupProduct;
using ApplicationFMS.Handlers.LookUp.LookupSector;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FmsWebUI.Controllers
{
    public class LookupController : BaseController
    {
        public async Task<IActionResult> Sector()
        {
            var response = await Mediator.Send(new LookupSectorListQuery());
            return Ok(response.List);
        }
        
        public async Task<IActionResult> Company(int id)
        {
            var response = await Mediator.Send(new LookupCompanyListQuery { SectorId = id });
            return Ok(response.List);
        }

        public async Task<IActionResult> Product(int id)
        {
            var response = await Mediator.Send(new LookupProductListQuery { CompanyId = id });
            return Ok(response.List);
        }

        public async Task<IActionResult> Type()
        {
            var response = await Mediator.Send(new LookupFeedbackTypeListQuery());
            return Ok(response.List);
        }

        public async Task<IActionResult> Education()
        {
            var response = await Mediator.Send(new LookupEducationListQuery());
            return Ok(response.List);
        }

        public async Task<IActionResult> City()
        {
            var response = await Mediator.Send(new LookupCityListQuery());
            return Ok(response.CityList);
        }

    }
}
