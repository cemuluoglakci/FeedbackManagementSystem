using MediatR;

namespace ApplicationFMS.Handlers.LookUp.LookupProduct
{
    public class LookupProductListQuery : IRequest<ProductListVm>
    {
        public int? CompanyId { get; set; }
    }
}
