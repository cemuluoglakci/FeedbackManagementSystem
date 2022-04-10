using System;
using System.Collections.Generic;

namespace CoreFMS.Entities
{
    public partial class UserCombined
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
        public int RoleId { get; set; }
        public string? RoleName { get; set; }
        public int? CompanyId { get; set; }
        public string? CompanyName { get; set; }
        public int? EducationId { get; set; }
        public string? EducationName { get; set; }
        public int? CityId { get; set; }
        public string? CityName { get; set; }
        public string? Iso { get; set; }
        public string? Iso3 { get; set; }
        public string? CountryName { get; set; }
        public short? Numcode { get; set; }
        public int? Phonecode { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? RegisteredAt { get; set; }
        public DateTime? LastLoginAt { get; set; }
        public DateTime? LastFailedLoginAt { get; set; }
        public int? FailedLoginAttemptCount { get; set; }
        public string Salt { get; set; } = null!;
        public string Hash { get; set; } = null!;
    }
}
