using ApplicationFMS.Helpers.Mappings;
using AutoMapper;
using CoreFMS.Entities;

namespace ApplicationFMS.Handlers.Feedbacks.Queries.GetPublicFeedbackDetail
{
    public class ReplyDto : IMapFrom<Reply>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Text { get; set; } = null!;
        public string? UserName { get; set; }
        public string? CompanyName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Reply, ReplyDto>()
                .ForMember(d => d.Id, opts => opts.MapFrom(s => s.Id))
                .ForMember(d => d.UserId, opts => opts.MapFrom(s => s.UserId))
                .ForMember(d => d.UserName, opts => opts.MapFrom(s => s.User.FirstName))
                .ForMember(d => d.CompanyName, opts => opts.MapFrom(s => s.User.Company.CompanyName))
                .ForMember(d => d.Text, opts => opts.MapFrom(s => s.Text));
        }
    }
}