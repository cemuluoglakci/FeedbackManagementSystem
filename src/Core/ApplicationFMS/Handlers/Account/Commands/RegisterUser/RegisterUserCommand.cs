using ApplicationFMS.Models;
using CoreFMS.Entities;
using MediatR;
using System;
using System.Text.Json.Serialization;

namespace ApplicationFMS.Handlers.Account.Commands.RegisterUser
{
    public class RegisterUserCommand : IRequest<BaseResponse>
    {
        public string Email { get; set; } = null!;
        public int PhoneCode { get; set; }
        public string? Phone { get; set; }
        [JsonIgnore]
        public string Password { get; set; } = null!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? CityId { get; set; }
        public int? EducationId { get; set; }
        public int RoleId { get; set; }
        public int? CompanyId { get; set; }

    }
}
