using ApplicationFMS.Handlers.Products.Commands.DeleteProduct;
using ApplicationFMS.Handlers.Products.Commands.UpsertProduct;
using ApplicationFMS.Helpers;
using ApplicationFMS.Models;
using FmsAPI.Helper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FmsAPI.Controllers
{
    [Authorize(Constants.CompanyRepresentativeRole)]
    public class ProductController : BaseController
    {
        [HttpPost]
        [Authorize(Constants.CompanyRepresentativeRole)]
        public async Task<ActionResult<BaseResponse<int>>> Upsert([FromBody] UpsertProductCommand request)
        {
            var vm = await Mediator.Send(request);
            return base.Ok(vm);
        }

        [HttpDelete("{id}")]
        [Authorize(Constants.CompanyRepresentativeRole)]
        public async Task<IActionResult> Delete(int id)
        {
            return base.Ok(await Mediator.Send(new DeleteProductCommand { Id = id }));
        }
    }
}
