using ApplicationFMS.Handlers.LookUp.LookupSector;
using ApplicationFMS.Helpers.Mappings;
using ApplicationFMS.Models;
using AutoMapper;
using CoreFMS.Entities;

namespace ApplicationFMS.Handlers.LookUp.LookupRole
{
    public class RoleDTO : BaseLookup, IMapFrom<Role>
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Role, RoleDTO>()
                .ForMember(d => d.Name, opts => opts.MapFrom(s => s.RoleName));
        }
    }
}
