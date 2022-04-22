using ApplicationFMS.Helpers;
using ApplicationFMS.Helpers.Mappings;
using AutoMapper;
using CoreFMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationFMS.Handlers.Feedbacks.Queries.GetPublicFeedbackDetail
{
    public class CommentDto : IMapFrom<Comment>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public int? ParentCommentId { get; set; }
        public string Text { get; set; } = null!;
        public int LikeCount { get; set; }
        public int DislikeCount { get; set; }
        public bool IsAnonym { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual ICollection<CommentDto> ChildComment { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Comment, CommentDto>()
                .ForMember(d => d.Id, opts => opts.MapFrom(s => s.Id))
                .ForMember(d => d.UserId, opts => opts.MapFrom(s => s.UserId))
                .ForMember(d => d.UserName, opt =>
                {
                    opt.MapFrom(s => s.IsAnonym ? "Anonym" : s.User.FirstName);
                })
                .ForMember(d => d.Text, opts => opts.MapFrom(s => s.Text));
        }
    }
}
