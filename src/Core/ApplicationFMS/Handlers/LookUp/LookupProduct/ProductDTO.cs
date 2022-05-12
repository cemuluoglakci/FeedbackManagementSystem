using ApplicationFMS.Helpers.Mappings;
using ApplicationFMS.Models;
using AutoMapper;

namespace ApplicationFMS.Handlers.LookUp.LookupProduct
{
    public class ProductDTO : BaseLookup, IMapFrom<CoreFMS.Entities.Product>
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CoreFMS.Entities.Product, ProductDTO>()
                .ForMember(d => d.Name, opts => opts.MapFrom(s => s.ProductName));
        }
    }
}
