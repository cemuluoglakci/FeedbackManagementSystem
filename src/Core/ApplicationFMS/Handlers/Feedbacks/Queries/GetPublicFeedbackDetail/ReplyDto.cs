using ApplicationFMS.Helpers;
using ApplicationFMS.Helpers.Mappings;
using AutoMapper;
using CoreFMS.Entities;
using System;

namespace ApplicationFMS.Handlers.Feedbacks.Queries.GetPublicFeedbackDetail
{
    public class ReplyDto : IMapFrom<Reply>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Text { get; set; } = null!;
        public string? UserName { get; set; }
        public int UserRoleId { get; set; }
        public string UserRole { get; set; }
        public string? CompanyName { get; set; }
        public DateTime CreatedAt { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Reply, ReplyDto>()
                .ForMember(d => d.UserName, opt =>
                {
                    opt.MapFrom(s => 
                        s.Feedback.IsAnonym && s.User.Role.RoleName == Constants.CustomerRole 
                        ? "Anonym" : s.User.FirstName );
                })
                .ForMember(d => d.UserRoleId, opts => opts.MapFrom(s => s.User.RoleId))
                .ForMember(d => d.UserRole, opts => opts.MapFrom(s => s.User.Role.RoleName))
                .ForMember(d => d.CompanyName, opts => opts.MapFrom(s => s.User.Company.CompanyName))
                ;
        }
    }
}