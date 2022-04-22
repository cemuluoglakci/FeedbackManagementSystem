using ApplicationFMS.Handlers.Feedbacks.Queries.GetPublicFeedbackDetail;
using ApplicationFMS.Helpers.Mappings;
using AutoMapper;
using CoreFMS.Entities;

namespace ApplicationFMS.Handlers.Feedbacks.Queries.GetAdminFeedbackDetail
{
    public class ReplyAdminDto : ReplyDto, IMapFrom<Reply>
    {
        public bool IsActive { get; set; }
        public bool IsChecked { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Reply, ReplyAdminDto>()
                .ForMember(d => d.UserName, opts => opts.MapFrom(s => s.User.FirstName))
                .ForMember(d => d.UserRoleId, opts => opts.MapFrom(s => s.User.RoleId))
                .ForMember(d => d.UserRole, opts => opts.MapFrom(s => s.User.Role.RoleName))
                .ForMember(d => d.CompanyName, opts => opts.MapFrom(s => s.User.Company.CompanyName))
                ;
        }
    }
}
