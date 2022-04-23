using ApplicationFMS.Handlers.LookUp.LookupFeedbackType;
using ApplicationFMS.Helpers.Mappings;
using ApplicationFMS.Models;
using AutoMapper;
using CoreFMS.Entities;

namespace ApplicationFMS.Handlers.LookUp.LookupMode
{
    public class ModeDTO : BaseLookup, IMapFrom<OperationMode>
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap<OperationMode, ModeDTO>()
                .ForMember(d => d.Name, opts => opts.MapFrom(s => s.ModeName));
        }
    }
}
