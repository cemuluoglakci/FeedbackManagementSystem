using ApplicationFMS.Handlers.Comments.Queries.GetCommentDetail;
using System.Collections.Generic;

namespace ApplicationFMS.Handlers.Comments.Queries.GetOwnCommentList
{
    public class GetOwnCommentListVm
    {
        public int Count { get; set; }
        public IList<CommentDetailDTO>? CommentList { get; set; }

    }
}
