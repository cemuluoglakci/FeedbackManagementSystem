using ApplicationFMS.Handlers.System.Commands.SetOperationalMode;
using ApplicationFMS.Handlers.System.Commands.SetTimeoutDuration;
using ApplicationFMS.Helpers;
using ApplicationFMS.Models;
using FmsAPI.Helper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FmsAPI.Controllers
{
    public class SystemController : BaseController
    {
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
