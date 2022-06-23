using ApplicationFMS.Interfaces;
using ApplicationFMS.Models;
using MediatR;
using System;

namespace ApplicationFMS.Handlers.UserHandlers.Queries.GetUserList
{
    public class GetUserListQuery : ISearchQuery, IRequest<BaseResponse>
    {
        public int ObjectsPerPage { get; set; } = 10;
        public int PageNumber { get; set; } = 1;
        public string SortColumn { get; set; } = "Id";
        public bool IsAscending { get; set; } = false;
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
