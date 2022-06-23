using ApplicationFMS.Handlers.Comments.Commands.UpsertComment;
using ApplicationFMS.Handlers.Comments.Queries.GetCommentDetail;
using ApplicationFMS.Handlers.Feedbacks.Commands.UpsertFeedback;
using ApplicationFMS.Handlers.Feedbacks.Queries.GetPublicFeedbackDetail;
using ApplicationFMS.Handlers.Feedbacks.Queries.GetPublicFeedbackList;
using AutoMapper;

namespace FmsWebUI.Models
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<PublicFeedbackDTO, UpsertFeedbackCommand>();
            CreateMap<GetPublicFeedbackDetailVm, UpsertFeedbackCommand>();
            CreateMap<CommentDetailDTO, UpsertCommentCommand>();
        }

    }
}
