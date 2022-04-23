using System.Collections.Generic;

namespace ApplicationFMS.Handlers.LookUp.LookupFeedbackType
{
    public class FeedbackTypeListVm
    {
        public IList<FeedbackTypeDTO>? List { get; set; }
        public int Count { get; set; }
    }
}
