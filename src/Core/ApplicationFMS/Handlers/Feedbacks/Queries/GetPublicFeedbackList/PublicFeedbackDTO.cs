using ApplicationFMS.Helpers.Mappings;
using AutoMapper;
using CoreFMS.Entities;
using System;

namespace ApplicationFMS.Handlers.Feedbacks.Queries.GetPublicFeedbackList
{
    public class PublicFeedbackDTO : IMapFrom<Feedback>
    {

        public int Id { get; set; }
        public int UserId { get; set; }
        public string? CustomerFirstName { get; set; }
        public string Title { get; set; } = null!;
        public string Text { get; set; } = null!;
        public int? SectorId { get; set; }
        public string SectorName { get; set; }
        public int? CompanyId { get; set; }
        public string CompanyName { get; set; }
        public int? ProductId { get; set; }
        public string ProductName { get; set; }
        public int TypeId { get; set; }
        public string TypeName { get; set; }
        public int? SubTypeId { get; set; }
        public string SubTypeName { get; set; }

        public int LikeCount { get; set; }
        public int DislikeCount { get; set; }
        public bool? UserReaction { get; set; }

        public DateTime CreatedAt { get; set; }
        public bool IsAnonym { get; set; }
        public bool? IsReplied { get; set; }
        public bool? IsSolved { get; set; }
        public bool IsMine { get; set; } = false;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Feedback, PublicFeedbackDTO>()
                .ForMember(d => d.UserId, opt =>
                {
                    opt.MapFrom(s => s.IsAnonym ? 0 : s.UserId);
                })

                .ForMember(d => d.CustomerFirstName, opt =>
                {
                    opt.MapFrom(s => s.IsAnonym ? "Anonym" : s.User.FirstName);
                })
                .ForMember(d => d.SectorName, opt =>
                {
                    opt.PreCondition(s => (s.Sector != null));
                    opt.MapFrom(s => s.Sector.SectorName);
                })
                .ForMember(d => d.CompanyName, opt =>
                {
                    opt.PreCondition(s => (s.Company != null));
                    opt.MapFrom(s => s.Company.CompanyName);
                })
                .ForMember(d => d.ProductName, opt =>
                {
                    opt.PreCondition(s => (s.Product != null));
                    opt.MapFrom(s => s.Product.ProductName);
                })
                .ForMember(d => d.TypeName, opt =>
                {
                    opt.MapFrom(s => (s.TypeId == 0) ? null : s.Type.TypeName);
                })
                .ForMember(d => d.SubTypeName, opt =>
                {
                    opt.PreCondition(s => (s.SubType != null));
                    opt.MapFrom(s => s.SubType.SubTypeName);
                })
                ;
        }

    }
}
