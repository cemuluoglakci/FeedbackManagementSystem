﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationFMS.Helpers
{
    public static class PreDefinedTypes
    {
        public static readonly string _adminRole = "System Administrator";
        public static readonly string _customerRole = "Customer";
        public static readonly string _companyEmployee = "Company Employee";
        public static readonly string _companyRepresentative = "Company Representative";
        public static readonly List<string> _companyRoles = new List<string> { "Company Representative", "Company Manager", "Company Employee" };


    }
}
