using ApplicationFMS.Handlers.Feedbacks.Queries.GetPublicFeedbackList;
using ApplicationFMS.Helpers.Mappings;
using AutoMapper;
using CoreFMS.Entities;
using System.Collections.Generic;

namespace ApplicationFMS.Handlers.Feedbacks.Queries.GetPublicFeedbackDetail
{
    public class GetPublicFeedbackDetailVm : PublicFeedbackDTO, IMapFrom<Feedback>
    {
        //public virtual ICollection<Comment> Comments { get; set; }
        //public virtual List<Reply> Replies { get; set; }
        public virtual List<ReplyDto> Replies { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Feedback, GetPublicFeedbackDetailVm>()

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
                //.ForMember(d => d.Comments, opt => opt.MapFrom(s => s.Comments))
                ;
        }
    }
}
