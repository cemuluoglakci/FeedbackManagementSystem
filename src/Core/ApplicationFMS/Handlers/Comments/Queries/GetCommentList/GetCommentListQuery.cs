using ApplicationFMS.Interfaces;
using ApplicationFMS.Models;
using MediatR;

namespace ApplicationFMS.Handlers.Comments.Queries.GetCommentList
{
    public class GetCommentListQuery : IPostSearchQuery, IRequest<BaseResponse>
    {
        public int ObjectsPerPage { get; set; } = 10;
        public int PageNumber { get; set; } = 1;
        public string? SortColumn { get; set; } = "Id";
        public bool IsAscending { get; set; } = false;
        public string? TextQuery { get; set; } = string.Empty;
        public bool? IsActive { get; set; }
        public bool? IsChecked { get; set; }

    }
}
