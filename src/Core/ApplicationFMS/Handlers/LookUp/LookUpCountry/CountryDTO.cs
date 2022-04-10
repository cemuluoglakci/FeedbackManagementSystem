using ApplicationFMS.Helpers.Mappings;
using AutoMapper;
using CoreFMS.Entities;

namespace ApplicationFMS.Handlers.LookUp.LookUpCountry
{
    public class CountryDTO : IMapFrom<Country>
    {
        public int Id { get; set; }
        public string Iso { get; set; } = null!;
        public string CountryName { get; set; } = null!;
        public string? Iso3 { get; set; }
        public int Phonecode { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Country, CountryDTO>();
        }
    }
}
