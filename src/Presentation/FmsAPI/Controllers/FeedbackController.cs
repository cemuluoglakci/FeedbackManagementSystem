using ApplicationFMS.Handlers.Feedbacks.Queries.GetPublicFeedbackList;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FmsAPI.Controllers
{
    public class FeedbackController : BaseController
    {
        [HttpPost]
        public async Task<ActionResult<PublicFeedbackListVm>> GetAll([FromBody]GetPublicFeedbackListQuery request)
        {
            var vm = await Mediator.Send(request);
            return base.Ok(vm);
        }
    }
}
