using System;
using System.Collections.Generic;

namespace CoreFMS.Entities
{
    public partial class City
    {
        public City()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public int? CountryId { get; set; }
        public string? CityName { get; set; }

        public virtual Country? Country { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
