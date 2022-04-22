using ApplicationFMS.Handlers.Feedbacks.Queries.GetPublicFeedbackDetail;
using ApplicationFMS.Helpers.Mappings;
using AutoMapper;
using CoreFMS.Entities;
using System.Collections.Generic;

namespace ApplicationFMS.Handlers.Feedbacks.Queries.GetAdminFeedbackDetail
{
    public class CommentAdminDto : CommentDto, IMapFrom<Comment>
    {
        public virtual ICollection<CommentAdminDto> ChildComment { get; set; }
        public bool IsActive { get; set; }
        public bool IsChecked { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Comment, CommentAdminDto>()
                .ForMember(d => d.UserName, opts => opts.MapFrom(s => s.User.FirstName));
        }
    }
}
