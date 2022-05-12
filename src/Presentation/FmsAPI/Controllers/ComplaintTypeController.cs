using ApplicationFMS.Handlers.ComplaintTypes.Commands.DeleteComplaintType;
using ApplicationFMS.Handlers.ComplaintTypes.Commands.UpsertComplaintType;
using ApplicationFMS.Helpers;
using ApplicationFMS.Models;
using FmsAPI.Helper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FmsAPI.Controllers
{
    [Authorize(Constants.AdminRole)]
    public class ComplaintTypeController : BaseController
    {
        [HttpPost]
        [Authorize(Constants.AdminRole)]
        public async Task<ActionResult<BaseResponse<int>>> Upsert([FromBody] UpsertComplaintTypeCommand request)
        {
            var vm = await Mediator.Send(request);
            return base.Ok(vm);
        }

        [HttpDelete("{id}")]
        [Authorize(Constants.AdminRole)]
        public async Task<IActionResult> Delete(int id)
        {
            return base.Ok(await Mediator.Send(new DeleteComplaintTypeCommand { Id = id }));
        }
    }
}
