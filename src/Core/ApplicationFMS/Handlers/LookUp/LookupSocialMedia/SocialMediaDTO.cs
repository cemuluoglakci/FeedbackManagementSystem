using ApplicationFMS.Helpers.Mappings;
using ApplicationFMS.Models;
using AutoMapper;
using CoreFMS.Entities;

namespace ApplicationFMS.Handlers.LookUp.LookupSocialMedia
{
    public class SocialMediaDTO : BaseLookup, IMapFrom<SocialMedium>
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap<SocialMedium, SocialMediaDTO>()
                .ForMember(d => d.Name, opts => opts.MapFrom(s => s.SocialMediaName));
        }
    }
}
