using System.Collections.Generic;

namespace ApplicationFMS.Handlers.LookUp.LookupRole
{
    public class RoleListVm
    {
        public IList<RoleDTO>? List { get; set; }
        public int Count { get; set; }
    }
}
