using System.Collections.Generic;

namespace ApplicationFMS.Handlers.LookUp.LookUpCountry
{
    public class CountryListVm
    {
        public IList<CountryDTO>? CountryList { get; set; }

        public int Count { get; set; }
    }
}
