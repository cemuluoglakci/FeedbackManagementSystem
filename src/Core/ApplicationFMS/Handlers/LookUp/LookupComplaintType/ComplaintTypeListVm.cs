using System.Collections.Generic;

namespace ApplicationFMS.Handlers.LookUp.LookupComplaintType
{
    public class ComplaintTypeListVm
    {
        public IList<ComplaintTypeDTO>? List { get; set; }
        public int Count { get; set; }
    }
}
