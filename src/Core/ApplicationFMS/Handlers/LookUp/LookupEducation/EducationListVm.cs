using System.Collections.Generic;

namespace ApplicationFMS.Handlers.LookUp.LookupEducation
{
    public class EducationListVm
    {
        public IList<EducationDTO>? List { get; set; }
        public int Count { get; set; }
    }
}
