using ApplicationFMS.Helpers.Mappings;
using ApplicationFMS.Models;
using AutoMapper;
using CoreFMS.Entities;

namespace ApplicationFMS.Handlers.LookUp.LookupSector
{
    public class SectorDTO : BaseLookup, IMapFrom<Sector>
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Sector, SectorDTO>()
                .ForMember(d => d.Name, opts => opts.MapFrom(s => s.SectorName));
        }
    }
}
