using ApplicationFMS.Handlers.Feedbacks.Queries.GetPublicFeedbackList;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FmsAPI.Controllers
{
    public class FeedbackController : BaseController
    {
        [HttpPost]
        public async Task<ActionResult<FeedbackListVm>> GetAll([FromBody]GetFeedbackListQuery request)
        {
            var vm = await Mediator.Send(request);
            return base.Ok(vm);
        }
    }
}
