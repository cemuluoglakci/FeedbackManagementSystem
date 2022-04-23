using System.Collections.Generic;

namespace ApplicationFMS.Handlers.LookUp.LookupEmployees
{
    public class EmployeeListVm
    {
        public IList<EmployeeDTO>? List { get; set; }
        public int Count { get; set; }
    }
}
