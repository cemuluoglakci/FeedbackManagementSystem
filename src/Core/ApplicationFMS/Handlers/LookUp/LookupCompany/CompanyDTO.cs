using ApplicationFMS.Helpers.Mappings;
using ApplicationFMS.Models;
using AutoMapper;
using CoreFMS.Entities;

namespace ApplicationFMS.Handlers.LookUp.LookupCompany
{
    public class CompanyDTO : BaseLookup, IMapFrom<Company>
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Company, CompanyDTO>()
                .ForMember(d => d.Name, opts => opts.MapFrom(s => s.CompanyName));
        }
    }
}
