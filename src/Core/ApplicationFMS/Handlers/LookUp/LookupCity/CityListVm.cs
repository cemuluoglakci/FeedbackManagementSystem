using System.Collections.Generic;

namespace ApplicationFMS.Handlers.LookUp.LookUpCity
{
    public class CityListVm
    {
        public IList<CityDTO> CityList { get; set; } = null!;

        public int Count { get; set; }
    }
}
