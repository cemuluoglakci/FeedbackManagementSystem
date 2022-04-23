using ApplicationFMS.Helpers.Mappings;
using ApplicationFMS.Models;
using AutoMapper;
using CoreFMS.Entities;

namespace ApplicationFMS.Handlers.LookUp.LookupEducation
{
    public class EducationDTO : BaseLookup, IMapFrom<Education>
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Education, EducationDTO>()
                .ForMember(d => d.Name, opts => opts.MapFrom(s => s.EducationName));
        }
    }
}
