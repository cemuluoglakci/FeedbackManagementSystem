using ApplicationFMS.Handlers.LookUp.LookUpCity;
using ApplicationFMS.Helpers.Mappings;
using AutoMapper;
using CoreFMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationFMS.Models
{
    public class ContextUser : IMapFrom<User>
    {
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
        public string Salt { get; set; } = null!;
        public string Hash { get; set; } = null!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? RegisteredAt { get; set; }
        public DateTime? LastLoginAt { get; set; }
        public DateTime? LastFailedLoginAt { get; set; }
        public int? FailedLoginAttemptCount { get; set; }
        public int? CityId { get; set; }
        public int? EducationId { get; set; }
        public int RoleId { get; set; }
        public int? CompanyId { get; set; }
        public int IsActive { get; set; }
        public string RoleName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, ContextUser>()
                .ForMember(d => d.RoleName, opt => opt.MapFrom(s => s.Role.RoleName));
        }
    }
}
