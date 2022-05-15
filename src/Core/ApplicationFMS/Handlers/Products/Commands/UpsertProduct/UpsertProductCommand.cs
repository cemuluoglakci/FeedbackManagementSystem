using ApplicationFMS.Models;
using MediatR;

namespace ApplicationFMS.Handlers.Products.Commands.UpsertProduct
{
    public class UpsertProductCommand : IRequest<BaseResponse>
    {
        public int? Id { get; set; }
        public string ProductName { get; set; }
    }
}
