using System.Collections.Generic;

namespace CoreFMS.Entities
{
    public partial class Country
    {
        public Country()
        {
            Cities = new HashSet<City>();
        }

        public int Id { get; set; }
        public string Iso { get; set; } = null!;
        public string CountryName { get; set; } = null!;
        public string Nicename { get; set; } = null!;
        public string? Iso3 { get; set; }
        public short? Numcode { get; set; }
        public int Phonecode { get; set; }

        public virtual ICollection<City> Cities { get; set; }
    }
}
