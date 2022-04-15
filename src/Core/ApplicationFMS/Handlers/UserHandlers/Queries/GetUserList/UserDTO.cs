using ApplicationFMS.Helpers.Mappings;
using ApplicationFMS.Models;
using AutoMapper;
using CoreFMS.Entities;
using System;

namespace ApplicationFMS.Handlers.UserHandlers.Queries.GetUserList
{
    public class UserDTO : IMapFrom<User>
    {

        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? RegisteredAt { get; set; }
        public DateTime? LastLoginAt { get; set; }
        public DateTime? LastFailedLoginAt { get; set; }
        public int? FailedLoginAttemptCount { get; set; }
        //public int? CityId { get; set; }
        public string CityName { get; set; }
        //public int? EducationId { get; set; }
        public string EducationName { get; set; }
        //public int RoleId { get; set; }
        public string RoleName { get; set; }
        //public int? CompanyId { get; set; }
        public string CompanyName { get; set; }
        public bool IsActive { get; set; }
        public bool IsTwoFactorAuth { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserDTO>()
                .ForMember(d => d.RoleName, opt => opt.MapFrom(s => s.Role.RoleName))
                .ForMember(d => d.CityName, opt => opt.MapFrom(s => s.City.CityName))
                .ForMember(d => d.EducationName, opt => opt.MapFrom(s => s.Education.EducationName))
                .ForMember(d => d.CompanyName, opt => opt.MapFrom(s => s.Company.CompanyName));
        }
    }
}
