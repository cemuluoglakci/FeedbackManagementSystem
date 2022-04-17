using ApplicationFMS.Helpers.Mappings;
using AutoMapper;
using CoreFMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationFMS.Handlers.Feedbacks.Queries.GetPublicFeedbackList
{
    public class AdminFeedbackDTO : CompanyFeedbackDTO, IMapFrom<Feedback>
    {
        public bool? IsActive { get; set; }
        public bool? IsChecked { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Feedback, AdminFeedbackDTO>()
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
                ;
        }


    }
}
