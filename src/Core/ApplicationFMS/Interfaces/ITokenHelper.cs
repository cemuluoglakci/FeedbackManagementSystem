using ApplicationFMS.Models;
using CoreFMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationFMS.Interfaces
{
    public interface ITokenHelper
    {
        public string GenerateJwtToken(User user);
        public int? ValidateJwtToken(string token);
    }
}
