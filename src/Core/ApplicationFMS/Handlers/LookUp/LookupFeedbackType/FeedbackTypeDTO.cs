using ApplicationFMS.Helpers.Mappings;
using ApplicationFMS.Models;
using AutoMapper;
using CoreFMS.Entities;

namespace ApplicationFMS.Handlers.LookUp.LookupFeedbackType
{
    public class FeedbackTypeDTO : BaseLookup, IMapFrom<FeedbackType>
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap<FeedbackType, FeedbackTypeDTO>()
                .ForMember(d => d.Name, opts => opts.MapFrom(s => s.TypeName));
        }
    }
}
