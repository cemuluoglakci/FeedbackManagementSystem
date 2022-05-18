using ApplicationFMS.Handlers.System.Commands.SetOperationalMode;
using ApplicationFMS.Handlers.System.Commands.SetTimeoutDuration;
using ApplicationFMS.Handlers.System.Queries.GetOperationalMode;
using ApplicationFMS.Helpers;
using ApplicationFMS.Models;
using FmsAPI.Helper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ApplicationFMS.Handlers.System.Queries.GetTimeoutDuration;
using ApplicationFMS.Handlers.System.Queries.GetLogs;

namespace FmsAPI.Controllers
{
    public class SystemController : BaseController
    {

        [HttpGet]
        [Authorize(Constants.AdminRole)]
        public async Task<ActionResult<BaseResponse>> GetModeOfOperation()
        {
            return base.Ok(await Mediator.Send(new GetOperationalModeQuery()));
        }

        [HttpGet]
        [Authorize(Constants.AdminRole)]
        public async Task<ActionResult<BaseResponse>> GetTimeoutDuration()
        {
            return base.Ok(await Mediator.Send(new GetTimeoutDurationQuery()));
        }

        [HttpPost]
        [Authorize(Constants.AdminRole)]
        public async Task<ActionResult<BaseResponse>> GetLogList([FromBody] GetLogListQuery request)
        {
            return base.Ok(await Mediator.Send(request));
        }

        [HttpPut]
        [Authorize(Constants.AdminRole)]
        public async Task<ActionResult<BaseResponse>> SetModeOfOperation([FromBody] SetOperationalModeCommand request)
        {
            return base.Ok(await Mediator.Send(request));
        }

        [HttpPut]
        [Authorize(Constants.AdminRole)]
        public async Task<ActionResult<BaseResponse>> SetTimeoutDuration([FromBody] SetTimeoutDurationCommand request)
        {
            return base.Ok(await Mediator.Send(request));
        }



    }
}
