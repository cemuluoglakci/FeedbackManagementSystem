using System.Collections.Generic;

namespace ApplicationFMS.Handlers.UserHandlers.Queries.GetUserList
{
    public class UserListVm
    {
        public int TotalCount { get; set; }
        public int FilteredCount { get; set; }
        public int ObjectsPerPage { get; set; }
        public int PageNumber { get; set; }
        public IList<UserDTO> UserList { get; set; }
    }
}
