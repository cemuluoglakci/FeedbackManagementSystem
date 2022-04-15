using ApplicationFMS.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationFMS.Handlers.UserHandlers.Queries.GetUserList
{
    public  class GetUserListQuery : IRequest<BaseResponse<UserListVm>>
    {
        public int ObjectsPerPage { get; set; } = 10;
        public int PageNumber { get; set; } = 1;
        public string? EmailQuery { get; set; } = string.Empty;
        public string? FirstNameQuery { get; set; } = string.Empty;
        public string? LastNameQuery { get; set; } = string.Empty;
        public DateTime? BirthDateBefore { get; set; }
        public DateTime? BirthDateAfter { get; set; }
        public DateTime? RegisteredAtBefore { get; set; }
        public DateTime? RegisteredAtAfter { get; set; }

        public int? CityId { get; set; }
        public int? EducationId { get; set; }
        public int? RoleId { get; set; }
        public int? CompanyId { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsTwoFactorAuth { get; set; }
    }
}
