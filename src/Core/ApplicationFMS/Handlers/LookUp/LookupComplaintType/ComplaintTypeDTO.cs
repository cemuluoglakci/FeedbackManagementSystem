using ApplicationFMS.Helpers.Mappings;
using ApplicationFMS.Models;
using AutoMapper;
using CoreFMS.Entities;

namespace ApplicationFMS.Handlers.LookUp.LookupComplaintType
{
    public class ComplaintTypeDTO : BaseLookup, IMapFrom<FeedbackSubType>
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap<FeedbackSubType, ComplaintTypeDTO>()
                .ForMember(d => d.Name, opts => opts.MapFrom(s => s.SubTypeName));
        }
    }
}
