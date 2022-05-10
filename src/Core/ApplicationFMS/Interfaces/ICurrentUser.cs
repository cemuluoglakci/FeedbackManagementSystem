using ApplicationFMS.Models;
using CoreFMS.Entities;

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

        public bool NotInRole(string roleName)
        {
            return !this.IsInRole(roleName);
        }
        public bool HasSameId(int id)
        {
            if (this.UserDetail.Id == id) { return true; }
            else { return false; }
        }

        public bool IsEligibleToReplyFeedback(Feedback feedback)
        {
            if (this.UserDetail.Id == feedback.DirectedToEmployeeId || (this.UserDetail.Id == feedback.UserId && feedback.IsReplied))
            {
                return true;
            }
            return false;
        }

        bool NotInCompany(int? companyId)
        {
            if (companyId == null) { return true; }
            if (this.UserDetail.CompanyId != companyId)
            {
                return true;
            }
            return false;
        }
    }
}
