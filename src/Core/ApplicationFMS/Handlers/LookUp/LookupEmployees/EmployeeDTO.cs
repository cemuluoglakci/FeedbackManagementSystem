using ApplicationFMS.Helpers.Mappings;
using AutoMapper;
using CoreFMS.Entities;

namespace ApplicationFMS.Handlers.LookUp.LookupEmployees
{
    public class EmployeeDTO : IMapFrom<User>
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, EmployeeDTO>()
                .ForMember(d => d.FullName, opts => opts.MapFrom(s => $"{s.FirstName} {s.LastName}"));
        }

    }
}
