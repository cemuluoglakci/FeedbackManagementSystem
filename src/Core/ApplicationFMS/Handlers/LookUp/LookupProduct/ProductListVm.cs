using System.Collections.Generic;

namespace ApplicationFMS.Handlers.LookUp.LookupProduct
{
    public class ProductListVm
    {
        public IList<ProductDTO>? List { get; set; }
        public int Count { get; set; }
    }
}
