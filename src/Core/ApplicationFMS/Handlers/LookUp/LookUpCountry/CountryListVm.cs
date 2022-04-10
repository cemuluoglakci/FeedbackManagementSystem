using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationFMS.Handlers.LookUp.LookUpCountry
{
    public class CountryListVm
    {
        public IList<CountryDTO>? CountryList { get; set; }

        public int Count { get; set; }
    }
}
