using ApplicationFMS.Handlers.LookUp.LookupFeedbackType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationFMS.Handlers.LookUp.LookupComplaintType
{
    public class ComplaintTypeListVm
    {
        public IList<ComplaintTypeDTO>? List { get; set; }
        public int Count { get; set; }
    }
}
