using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationFMS.Helpers
{
    public class Constants
    {
        public const string AdminRole = "System Administrator";
        public const string CustomerRole = "Customer";
        public const string CompanyEmployeeRole = "Company Employee";
        public const string CompanyRepresentativeRole = "Company Representative";
        public const string CompanyManagerRole = "Company Manager";
        public readonly static List<string> CompanyRoles = new List<string> { "Company Representative", "Company Manager", "Company Employee" };
    }
}
