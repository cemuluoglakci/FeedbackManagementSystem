using ApplicationFMS.Handlers.Report.CustomerStatistics;
using ApplicationFMS.Handlers.Report.EmployeeReport;
using ApplicationFMS.Handlers.Report.FeedbackCounts;
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

        [HttpGet]
        public async Task<ActionResult<BaseResponse<EmployeeReportVm>>> EmployeeReport()
        {
            return base.Ok(await Mediator.Send(new EmployeeReportQuery { }));
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponse<CustomerStatisticsVm>>> CustomerStatistics(int productId, int typeId)
        {
            return base.Ok(await Mediator.Send(new CustomerStatisticsQuery { ProductId = productId, TypeId = typeId }));
        }

    }
}
