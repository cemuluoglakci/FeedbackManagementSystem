using ApplicationFMS.Handlers.Feedbacks.Queries.GetPublicFeedbackDetail;
using ApplicationFMS.Handlers.Feedbacks.Queries.GetPublicFeedbackList;
using ApplicationFMS.Helpers.Mappings;
using AutoMapper;
using CoreFMS.Entities;

namespace ApplicationFMS.Handlers.Comments.Queries.GetCommentDetail
{
    public class CommentDetailDTO : CommentDto, IMapFrom<Comment>
    {
        public bool IsMine { get; set; }
        public PublicFeedbackDTO FeedbackDTO { get; set; }

        public new void Mapping(Profile profile)
        {
            profile.CreateMap<Comment, CommentDetailDTO>()
                .ForMember(d => d.UserName, opt =>
                {
                    opt.MapFrom(s => s.IsAnonym ? "Anonym" : s.User.FirstName);
                })
                .ForMember(dest => dest.FeedbackDTO, opt => opt.MapFrom(src => src.Feedback))
                ;
        }

    }
}
