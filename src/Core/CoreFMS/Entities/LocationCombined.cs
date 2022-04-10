using System;
using System.Collections.Generic;

namespace CoreFMS.Entities
{
    public partial class LocationCombined
    {
        public int Id { get; set; }
        public string? CityName { get; set; }
        public string? Iso { get; set; }
        public string? Iso3 { get; set; }
        public string? CountryName { get; set; }
        public short? Numcode { get; set; }
        public int? Phonecode { get; set; }
    }
}
