using ApplicationFMS.Handlers.Comments.Queries.GetCommentDetail;
using System.Collections.Generic;

namespace ApplicationFMS.Handlers.Comments.Queries.GetCommentList
{
    public class GetCommentListVm
    {
        public int TotalCount { get; set; }
        public int FilteredCount { get; set; }
        public int ObjectsPerPage { get; set; }
        public int PageNumber { get; set; }
        public IList<CommentDetailDTO>? CommentList { get; set; }

    }
}
