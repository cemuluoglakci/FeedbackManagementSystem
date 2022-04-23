using ApplicationFMS.Helpers.Mappings;
using ApplicationFMS.Models;
using AutoMapper;
using CoreFMS.Entities;

namespace ApplicationFMS.Handlers.LookUp.LookupProduct
{
    public class ProductDTO : BaseLookup, IMapFrom<Product>
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Product, ProductDTO>()
                .ForMember(d => d.Name, opts => opts.MapFrom(s => s.ProductName));
        }
    }
}
