using ApplicationFMS.Helpers.Mappings;
using AutoMapper;
using CoreFMS.Entities;

namespace ApplicationFMS.Handlers.LookUp.LookUpCity
{
    public class CityDTO : IMapFrom<City>
    {
        public int Id { get; set; }
        public string CityName { get; set; } = null!;
        public int? CountryId { get; set; } = null!;
        public string CountryName { get; set; } = null!;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<City, CityDTO>()
                .ForMember(d => d.CountryName, opt => opt.MapFrom(s => s.Country.CountryName));
        }
    }
}
