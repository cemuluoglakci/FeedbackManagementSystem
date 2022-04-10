using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationFMS.Handlers.LookUp.LookUpCity
{
    public class CityListVm
    {
        public IList<CityDTO> CityList { get; set; } = null!;

        public int Count { get; set; }
    }
}
