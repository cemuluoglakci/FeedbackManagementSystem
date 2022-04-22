using ApplicationFMS.Handlers.Feedbacks.Queries.GetPublicFeedbackDetail;
using ApplicationFMS.Handlers.Feedbacks.Queries.GetPublicFeedbackList;
using ApplicationFMS.Helpers.Mappings;
using AutoMapper;
using CoreFMS.Entities;
using System.Collections.Generic;
using System.Linq;

namespace ApplicationFMS.Handlers.Feedbacks.Queries.GetAdminFeedbackDetail
{
    public class GetAdminFeedbackDetailVm : AdminFeedbackDTO, IMapFrom<Feedback>
    {
        public virtual List<ReplyAdminDto> ReplyList { get; set; }
        public virtual List<CommentAdminDto> CommentList { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Feedback, GetAdminFeedbackDetailVm>()
                .ForMember(d => d.CustomerFirstName, opt => opt.MapFrom(s => s.User.FirstName))
                .ForMember(d => d.CustomerEmail, opt => opt.MapFrom(s => s.User.Email))
                .ForMember(d => d.CustomerPhone, opt => opt.MapFrom(s => s.User.Phone))
                .ForMember(d => d.CustomerLastName, opt => opt.MapFrom(s => s.User.LastName))

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
                .ForMember(d => d.ReplyList, opts => opts.MapFrom(s => s.Reply))
                .ForMember(d => d.CommentList, opts =>
                    opts.MapFrom(s => s.Comments.Where(i => i.ParentComment == null)))
                ;
        }

    }
}
