using System.Collections.Generic;

namespace ApplicationFMS.Handlers.LookUp.LookupSector
{
    public class SectorListVm
    {
        public IList<SectorDTO>? List { get; set; }
        public int Count { get; set; }
    }
}
