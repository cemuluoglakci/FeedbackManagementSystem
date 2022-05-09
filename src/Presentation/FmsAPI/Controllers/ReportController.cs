using ApplicationFMS.Handlers.Report.FeedbackCounts;
using ApplicationFMS.Handlers.UserHandlers.Commands.ToggleUserAbility;
using ApplicationFMS.Helpers;
using ApplicationFMS.Models;
using FmsAPI.Helper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FmsAPI.Controllers
{
    [Authorize(Constants.CompanyManagerRole)]
    public class ReportController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<BaseResponse<FeedbackCountsVm>>> FeedbackCounts(int productId, int typeId)
        {
            return base.Ok(await Mediator.Send(new FeedbackCountsQuery { ProductId = productId, TypeId = typeId }));
        }
    }
}
