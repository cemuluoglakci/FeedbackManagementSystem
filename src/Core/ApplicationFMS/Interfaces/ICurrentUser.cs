using ApplicationFMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationFMS.Interfaces
{
    public interface ICurrentUser
    {
        ContextUser UserDetail { get; }

        string RequestScheme { get; }
        string RequestHost { get; }
        string RequestPath { get; }
        string RequestQueryString { get; }

        public bool IsInRole(string roleName)
        {
            if (this.UserDetail.RoleName == roleName) { return true; }
            else { return false; }
        }
        public bool HasSameId(int id)
        {
            if (this.UserDetail.Id == id) { return true; }
            else { return false; }
        }
    }
}
