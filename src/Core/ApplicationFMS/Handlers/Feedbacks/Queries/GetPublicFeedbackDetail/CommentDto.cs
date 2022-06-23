using ApplicationFMS.Helpers.Mappings;
using AutoMapper;
using CoreFMS.Entities;
using System;
using System.Collections.Generic;

namespace ApplicationFMS.Handlers.Feedbacks.Queries.GetPublicFeedbackDetail
{
    public class CommentDto : IMapFrom<Comment>
    {
        public int Id { get; set; }
        public int FeedbackId { get; set; }
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public int? ParentCommentId { get; set; }
        public string Text { get; set; } = null!;
        public int LikeCount { get; set; }
        public int DislikeCount { get; set; }
        public bool IsAnonym { get; set; }
        public bool IsActive { get; set; }
        public bool IsChecked { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual ICollection<CommentDto> ChildComment { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Comment, CommentDto>()
                .ForMember(d => d.UserName, opt =>
                {
                    opt.MapFrom(s => s.IsAnonym ? "Anonym" : s.User.FirstName);
                });
        }
    }
}
